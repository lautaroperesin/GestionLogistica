using GestionLogisticaApp.ViewModels;
using GestionLogisticaBackend.DTOs.Usuario;
using GestionLogisticaApp.Pages;

namespace GestionLogisticaApp
{
    public partial class AppShell : Shell
    {
        public AppShellViewModel ViewModel => (AppShellViewModel)BindingContext;

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("EnvioDetallePage", typeof(EnvioDetallePage));
            Routing.RegisterRoute("CrearEnvioPage", typeof(CrearEnvioPage));
        }

        // Método público para cambiar el estado de login desde otras páginas
        public void SetLoginState(bool isLoggedIn)
        {
            ViewModel.SetLoginState(isLoggedIn);
        }

        public void SetUserLogin(UsuarioDto usuario)
        {
            ViewModel.SetUserLogin(usuario);
        }
    }
}
