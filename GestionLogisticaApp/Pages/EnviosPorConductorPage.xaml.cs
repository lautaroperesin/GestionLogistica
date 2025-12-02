using GestionLogisticaApp.ViewModels;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Envio;

namespace GestionLogisticaApp
{
    public partial class EnviosPorConductorPage : ContentPage
    {
        public IRelayCommand<EnvioDto> ViewDetalleCommand => (BindingContext as EnviosPorConductorViewModel)?.ViewDetalleCommand;

        public EnviosPorConductorPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Recargar los datos cada vez que la página aparece
            if (BindingContext is EnviosPorConductorViewModel viewModel)
            {
                await viewModel.InitializeAsync();
            }
        }
    }
}