using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaBackend.Services.Interfaces;
using Service.Interfaces;
using Service.Services;
using Shared.ApiServices;

namespace GestionLogisticaApp.ViewModels
{
    public partial class RecuperarPasswordViewModel : ObservableObject
    {
        AuthApiService _authService = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnviarCommand))]
        private string mail = string.Empty;

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private string successMessage = string.Empty;

        public IRelayCommand EnviarCommand { get; }
        public IRelayCommand VolverCommand { get; }

        public RecuperarPasswordViewModel()
        {
            EnviarCommand = new AsyncRelayCommand(OnEnviar, CanEnviar);
            VolverCommand = new AsyncRelayCommand(OnVolver);
        }

        private bool CanEnviar()
        {
            return !string.IsNullOrWhiteSpace(Mail);
        }

        private async Task OnEnviar()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                SuccessMessage = string.Empty;

                // Validación básica de email
                if (!Mail.Contains("@") || !Mail.Contains("."))
                {
                    ErrorMessage = "Por favor, ingrese un correo electrónico válido";
                    return;
                }

                UsuarioDto loginReset = new UsuarioDto
                {
                    Email = Mail,
                    Password = "" // Placeholder, el backend debe manejar esto adecuadamente
                };

                await _authService.ResetPassword(loginReset);


                await OnVolver();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al enviar las instrucciones: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnVolver()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}