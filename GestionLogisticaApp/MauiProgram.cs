using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using Shared.ApiServices;
using GestionLogisticaApp.ViewModels;

namespace GestionLogisticaApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registrar servicios API sin AddHttpClient para evitar depender de Microsoft.Extensions.Http
            //builder.Services.AddTransient<EnvioApiService>();

            // Registrar ViewModels
            //builder.Services.AddTransient<EnviosViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
