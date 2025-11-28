using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.Enums;
using Shared.ApiServices;
using CommunityToolkit.Mvvm.Messaging;

namespace GestionLogisticaApp.ViewModels
{
    public partial class EnvioDetalleViewModel : ObservableObject
    {
        private readonly EnvioApiService _envioService = new();

        [ObservableProperty]
        private EnvioDto envio;

        [ObservableProperty]
        private EstadoEnvioEnum selectedEstado;

        [ObservableProperty]
        private string message = "";

        [ObservableProperty]
        private bool isBusy;

        public List<EstadoEnvioEnum> EstadosPosibles { get; } = Enum.GetValues(typeof(EstadoEnvioEnum)).Cast<EstadoEnvioEnum>().ToList();

        public IAsyncRelayCommand UpdateEstadoCommand { get; }

        public EnvioDetalleViewModel(int envioId)
        {
            Envio = new EnvioDto();
            UpdateEstadoCommand = new AsyncRelayCommand(UpdateEstadoAsync);
            LoadEnvioAsync(envioId);
        }

        private async void LoadEnvioAsync(int id)
        {
            if (IsBusy) return;
            
            try
            {
                IsBusy = true;
                var envio = await _envioService.GetEnvioByIdAsync(id);
                if (envio != null)
                {
                    Envio = envio;
                    SelectedEstado = envio.Estado;
                }
            }
            catch (Exception ex)
            {
                // handle error
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task UpdateEstadoAsync()
        {
            if (IsBusy) return;
            
            try
            {
                IsBusy = true;
                await _envioService.UpdateEnvioEstadoAsync(Envio.IdEnvio, SelectedEstado);
                Envio.Estado = SelectedEstado;
                OnPropertyChanged(nameof(Envio));
                Message = "Estado actualizado correctamente";
                WeakReferenceMessenger.Default.Send(new EnvioUpdatedMessage(Envio.IdEnvio));
                // Clear message after 3 seconds
                await Task.Delay(3000);
                Message = "";
            }
            catch (Exception ex)
            {
                // handle error
                Debug.WriteLine(ex.Message);
                Message = "Error al actualizar el estado";
                await Task.Delay(3000);
                Message = "";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    public class EnvioUpdatedMessage
    {
        public int EnvioId { get; }
        public EnvioUpdatedMessage(int envioId) => EnvioId = envioId;
    }
}