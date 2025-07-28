using System.Text;
using GestionLogisticaBackend.Services.Implementations;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IConductorService, ConductorService>();
builder.Services.AddScoped<IEnvioService, EnvioService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<IMovimientoCajaService, MovimientoCajaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<DashboardService>();

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

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "La API está corriendo");

app.Run();
