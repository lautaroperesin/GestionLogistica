using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Usuario;
using Microsoft.Maui.Controls;
using Shared.ApiServices;
using GestionLogisticaApp.Pages;

namespace GestionLogisticaApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        AuthApiService _authService;
        UsuarioApiService _usuarioService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string username = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string password = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private bool isBusy;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public IRelayCommand LoginCommand { get; }
        public IRelayCommand ForgotPasswordCommand { get; }
        public IRelayCommand RegisterCommand { get; }

        public LoginViewModel()
        {
            _authService = new AuthApiService();
            _usuarioService = new UsuarioApiService();
            LoginCommand = new RelayCommand(OnLogin, CanLogin);
            ForgotPasswordCommand = new RelayCommand(OnForgotPassword);
            RegisterCommand = new RelayCommand(OnRegister);
        }

        private bool CanLogin()
        {
            return !IsBusy && 
                   !string.IsNullOrWhiteSpace(Username) && 
                   !string.IsNullOrWhiteSpace(Password);
        }

        private async void OnLogin()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var response = await _authService.Login(new UsuarioDto
                {
                    Email = Username,
                    Password = Password
                });

                if (response != null)
                {
                    // Si la respuesta no es null, significa que hubo un error
                    ErrorMessage = response;
                    return;
                }

                var usuario = await _usuarioService.GetUserByEmailAsync(username);
                if (usuario == null)
                {
                    ErrorMessage = "No se pudo obtener la información del usuario.";
                    return;
                }
                Preferences.Set("UserLoginId", usuario.Id);

                if (Application.Current?.MainPage is AppShell shell)
                {
                    shell.SetLoginState(true);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al iniciar sesión: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnForgotPassword()
        {
            // Navegar a la página de recuperación o mostrar diálogo
            await Application.Current?.MainPage?.DisplayAlert("Recuperar contraseña", "Funcionalidad de recuperar contraseña no implementada aún.", "OK");
        }

        private async void OnRegister()
        {
            // Navegar a la página de registro
            await Application.Current?.MainPage?.Navigation?.PushAsync(new RegistrarsePage());
        }

        partial void OnErrorMessageChanged(string? oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                Application.Current?.MainPage?.DisplayAlert("Error de inicio de sesión", newValue, "OK");
            }
        }
    }
}
