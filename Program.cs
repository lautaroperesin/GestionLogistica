using GestionLogisticaBackend.Services.Implementations;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:8080" };

        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IConductorService, ConductorService>();
builder.Services.AddScoped<IEnvioService, EnvioService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();

// Configurar Entity Framework con MySQL
builder.Services.AddDbContext<LogisticaContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "La API está corriendo");

app.Run();
