using CurrieTechnologies.Razor.SweetAlert2;
using GestionLogisticaBackend.Services.Interfaces;
using GestionLogisticaWeb.Components;
using Service.Interfaces;
using Service.Services;
using Shared.ApiServices;
using WebBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<FirebaseAuthService>();
builder.Services.AddScoped(typeof(IGenericApiService<>), typeof(GenericApiService<>));
builder.Services.AddScoped<IEnvioService, EnvioApiService>();
builder.Services.AddSweetAlert2();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
