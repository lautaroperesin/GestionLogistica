using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using GestionLogisticaBackend.DTOs.Envio;
using Shared.ApiServices;

namespace GestionLogisticaApp.ViewModels
{
    public partial class EnvioDetalleViewModel : ObservableObject
    {
        private readonly EnvioApiService _envioService = new();

        [ObservableProperty]
        private EnvioDto envio;

        public EnvioDetalleViewModel(int envioId)
        {
            Envio = new EnvioDto();
            LoadEnvioAsync(envioId);
        }

        private async void LoadEnvioAsync(int id)
        {
            try
            {
                var envio = await _envioService.GetEnvioByIdAsync(id);
                if (envio != null)
                {
                    Envio = envio;
                }
            }
            catch (Exception ex)
            {
                // handle error
                Debug.WriteLine(ex.Message);
            }
        }
    }
}