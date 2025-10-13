using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Usuario;
using Microsoft.Maui.Controls;

namespace GestionLogisticaApp.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoggedIn;

        public UsuarioDto? Usuario { get; private set; }


        public IRelayCommand LogoutCommand { get; }

        public AppShellViewModel()
        {
            LogoutCommand = new RelayCommand(OnLogout);
            SetLoginState(false); // Inicialmente no está logueado
        }

        public void SetLoginState(bool isLoggedIn)
        {
            if (Application.Current?.MainPage is AppShell shell)
            {
                if (isLoggedIn)
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                else
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;

                IsLoggedIn = isLoggedIn;
                if (isLoggedIn)
                    Shell.Current.GoToAsync("//MainPage");  // Cambio a MainPage (pantalla de inicio)
                else
                    Shell.Current.GoToAsync("//LoginPage");
            }

        }

        public void SetUserLogin(UsuarioDto usuario)
        {
            Usuario = usuario;
        }

        private void OnLogout()
        {
            SetLoginState(false);
        }
    }
}