using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Filters;
using GestionLogisticaBackend.Enums;
using Shared.ApiServices;
using Service.Services;

namespace GestionLogisticaApp.ViewModels
{
    public partial class EnviosPorConductorViewModel : ObservableObject
    {
        private readonly EnvioApiService _envioService;
        private readonly ConductorApiService _conductorService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<ConductorDto> conductores = new();

        [ObservableProperty]
        private ConductorDto? selectedConductor;

        [ObservableProperty]
        private ObservableCollection<EnvioDto> enviosPendientes = new();

        [ObservableProperty]
        private ObservableCollection<EnvioDto> enviosEnTransito = new();

        [ObservableProperty]
        private ObservableCollection<EnvioDto> enviosEntregados = new();

        [ObservableProperty]
        private ObservableCollection<EnvioDto> enviosDemorados = new();

        [ObservableProperty]
        private ObservableCollection<EnvioDto> enviosCancelados = new();

        public IRelayCommand LoadConductoresCommand { get; }
        public IRelayCommand ConductorSelectedCommand { get; }

        public EnviosPorConductorViewModel()
        {
            _envioService = new EnvioApiService();
            _conductorService = new ConductorApiService();

            LoadConductoresCommand = new RelayCommand(async () => await LoadConductoresAsync());
            ConductorSelectedCommand = new RelayCommand(async () => await LoadEnviosForSelectedConductorAsync());
            InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            await LoadConductoresAsync();
        }

        private async Task LoadConductoresAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                Conductores.Clear();

                var list = await _conductorService.GetAllConductoresAsync(pageNumber: 1, pageSize: 1000);
                if (list != null)
                {
                    foreach (var c in list)
                        Conductores.Add(c);
                }
            }
            catch (System.Exception ex)
            {
                // manejar error, por ahora dejamos en Debug
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadEnviosForSelectedConductorAsync()
        {
            if (IsBusy) return;
            if (SelectedConductor == null) return;

            try
            {
                IsBusy = true;

                EnviosPendientes.Clear();
                EnviosEnTransito.Clear();
                EnviosEntregados.Clear();
                EnviosDemorados.Clear();
                EnviosCancelados.Clear();

                var filtros = new EnvioFilterDto
                {
                    IdConductor = SelectedConductor.IdConductor,
                    PageNumber = 1,
                    PageSize = 1000
                };

                var result = await _envioService.GetEnviosAsync(filtros);
                if (result != null && result.Items != null)
                {
                    var envios = result.Items;

                    foreach (var e in envios)
                    {
                        switch (e.Estado)
                        {
                            case EstadoEnvioEnum.Pendiente:
                                EnviosPendientes.Add(e);
                                break;
                            case EstadoEnvioEnum.EnTransito:
                                EnviosEnTransito.Add(e);
                                break;
                            case EstadoEnvioEnum.Entregado:
                                EnviosEntregados.Add(e);
                                break;
                            case EstadoEnvioEnum.Demorado:
                                EnviosDemorados.Add(e);
                                break;
                            case EstadoEnvioEnum.Cancelado:
                                EnviosCancelados.Add(e);
                                break;
                            default:
                                // ignorar None u otros
                                break;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
