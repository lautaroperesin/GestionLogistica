using System.Diagnostics;
using GestionLogisticaApp.ViewModels;
using Microsoft.Maui.Controls;

namespace GestionLogisticaApp;

[QueryProperty(nameof(EnvioId), "envioId")]
public partial class EnvioDetallePage : ContentPage
{
    public string EnvioId { get; set; }

    public EnvioDetallePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (!string.IsNullOrEmpty(EnvioId) && int.TryParse(EnvioId, out int id))
        {
            BindingContext = new EnvioDetalleViewModel(id);
        }
    }
}