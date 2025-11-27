using System.Text;
using System.Text.Json.Serialization;
using Backend.Class;
using FirebaseAdmin;
using GestionLogisticaBackend.Implementations;
using GestionLogisticaBackend.Services;
using GestionLogisticaBackend.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using LogisticaBackend.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using Shared.Interfaces;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Configurar logging mejorado
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var firebaseJson = Environment.GetEnvironmentVariable("GOOGLE_CREDENTIALS");

if (string.IsNullOrWhiteSpace(firebaseJson))
{
    throw new Exception("Falta la variable GOOGLE_CREDENTIALS");
}

var credential = GoogleCredential.FromJson(firebaseJson);

FirebaseApp.Create(new AppOptions
{
    Credential = credential
});

builder.Services
    .AddAuthentication("Firebase")
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>("Firebase", null);

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IConductorService, ConductorService>();
builder.Services.AddScoped<IEnvioService, EnvioService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IMovimientoCajaService, MovimientoCajaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
//builder.Services.AddScoped<IAuthService, Auth>();
builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddScoped<DashboardService>();

// Configurar correctamente para que funcione tanto en Development como en Production
var connectionString = builder.Configuration.GetConnectionString("mysqlRemoto");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'mysqlRemoto' no está configurada.");
}

builder.Services.AddDbContext<LogisticaContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Agregar esquema de seguridad JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT en este formato: Bearer {su token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .WithOrigins("https://gestionlogisticabackend.azurewebsites.net", "https://localhost:8000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Habilitar Swagger en todos los entornos para facilitar pruebas en Azure
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Endpoint público de salud para verificar que la API funciona
app.MapGet("/", () => Results.Ok(new 
{ 
    status = "OK", 
    message = "Gestión Logística Backend API está funcionando correctamente",
    version = "1.0",
    environment = app.Environment.EnvironmentName,
    swagger = "/swagger",
    timestamp = DateTime.UtcNow
})).AllowAnonymous();

// Endpoint de diagnóstico (solo mostrar info sensible en Development)
app.MapGet("/health", (IConfiguration config, IWebHostEnvironment env) =>
{
    var hasConnectionString = !string.IsNullOrEmpty(config.GetConnectionString("mysqlRemoto"));
    var hasFirebaseConfig = !string.IsNullOrEmpty(config["ApiKeyFirebase"]);
    var hasGoogleCredentials = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_CREDENTIALS"));
    
    return Results.Ok(new
    {
        status = "Healthy",
        environment = env.EnvironmentName,
        checks = new
        {
            connectionString = hasConnectionString ? "Configurado" : "No configurado",
            firebaseConfig = hasFirebaseConfig ? "Configurado" : "No configurado",
            googleCredentials = hasGoogleCredentials ? "Configurado" : "No configurado"
        },
        timestamp = DateTime.UtcNow
    });
}).AllowAnonymous();

app.Run();
