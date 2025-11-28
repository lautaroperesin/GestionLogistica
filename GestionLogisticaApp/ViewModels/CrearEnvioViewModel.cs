using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Pagination;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.DTOs.Vehiculo;
using GestionLogisticaBackend.Enums;
using Service.Services;
using Shared.ApiServices;

namespace GestionLogisticaApp.ViewModels
{
    public partial class CrearEnvioViewModel : ObservableObject
    {
        private readonly EnvioApiService _envioService;
        private readonly ClienteApiService _clienteService;
        private readonly ConductorApiService _conductorService;
        private readonly VehiculoApiService _vehiculoService;
        private readonly UbicacionApiService _ubicacionService;
        private readonly GenericApiService<TipoCargaDto> _tipoCargaService = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string message = "";

        [ObservableProperty]
        private bool isMessageError;

        // Listas para los Pickers
        [ObservableProperty]
        private ObservableCollection<ClienteDto> clientes = new();

        [ObservableProperty]
        private ObservableCollection<ConductorDto> conductores = new();

        [ObservableProperty]
        private ObservableCollection<VehiculoDto> vehiculos = new();

        [ObservableProperty]
        private ObservableCollection<UbicacionDto> ubicaciones = new();

        [ObservableProperty]
        private ObservableCollection<TipoCargaDto> tiposCarga = new();

        // Propiedades del formulario
        [ObservableProperty]
        private ClienteDto? selectedCliente;

        [ObservableProperty]
        private ConductorDto? selectedConductor;

        [ObservableProperty]
        private VehiculoDto? selectedVehiculo;

        [ObservableProperty]
        private UbicacionDto? selectedOrigen;

        [ObservableProperty]
        private UbicacionDto? selectedDestino;

        [ObservableProperty]
        private TipoCargaDto? selectedTipoCarga;

        [ObservableProperty]
        private string numeroSeguimiento = string.Empty;

        [ObservableProperty]
        private DateTime fechaSalida = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private decimal pesoKg = 0;

        [ObservableProperty]
        private decimal volumenM3 = 0;

        [ObservableProperty]
        private string descripcion = string.Empty;

        [ObservableProperty]
        private decimal costoTotal = 0;

        [ObservableProperty]
        private EstadoEnvioEnum estadoEnvio = EstadoEnvioEnum.Pendiente;

        public IAsyncRelayCommand CrearEnvioCommand { get; }
        public IRelayCommand CancelarCommand { get; }
        public IRelayCommand GenerarNumeroSeguimientoCommand { get; }

        public CrearEnvioViewModel()
        {
            _envioService = new EnvioApiService();
            _clienteService = new ClienteApiService();
            _conductorService = new ConductorApiService();
            _vehiculoService = new VehiculoApiService();
            _ubicacionService = new UbicacionApiService();

            CrearEnvioCommand = new AsyncRelayCommand(CrearEnvioAsync);
            CancelarCommand = new RelayCommand(Cancelar);
            GenerarNumeroSeguimientoCommand = new RelayCommand(GenerarNumeroSeguimiento);

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await LoadDataAsync();
            GenerarNumeroSeguimiento();
        }

        private async Task LoadDataAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var pagParams = new PaginationParams { PageNumber = 1, PageSize = 1000 };

                // Cargar todas las listas en paralelo
                var clientesTask = _clienteService.GetClientesAsync(pagParams);
                var conductoresTask = _conductorService.GetConductoresAsync(pagParams);
                var vehiculosTask = _vehiculoService.GetVehiculosAsync();
                var ubicacionesTask = _ubicacionService.GetUbicacionesAsync(pagParams);
                var tiposCargaTask = LoadTiposCargaAsync();

                await Task.WhenAll(clientesTask, conductoresTask, vehiculosTask, ubicacionesTask, tiposCargaTask);

                Clientes.Clear();
                foreach (var c in clientesTask.Result.Items)
                    Clientes.Add(c);

                Conductores.Clear();
                foreach (var c in conductoresTask.Result.Items)
                    Conductores.Add(c);

                Vehiculos.Clear();
                foreach (var v in vehiculosTask.Result)
                    Vehiculos.Add(v);

                Ubicaciones.Clear();
                foreach (var u in ubicacionesTask.Result.Items)
                    Ubicaciones.Add(u);

                TiposCarga.Clear();
                foreach (var tc in tiposCargaTask.Result)
                    TiposCarga.Add(tc);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar datos: {ex.Message}");
                ShowErrorMessage("Error al cargar los datos necesarios");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<List<TipoCargaDto>> LoadTiposCargaAsync()
        {
            // Usa el generic service para obtener los tipos de carga
            var result = await _tipoCargaService.GetAllAsync();
            return result.ToList();
        }

        private void GenerarNumeroSeguimiento()
        {
            var random = new Random();
            var prefix = "ENV";
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var randomNumber = random.Next(1000, 9999);
            NumeroSeguimiento = $"{prefix}-{timestamp}-{randomNumber}";
        }

        private async Task CrearEnvioAsync()
        {
            if (IsBusy) return;

            if (!ValidarFormulario())
                return;

            try
            {
                IsBusy = true;
                Message = "";

                // Crear EnvioDto igual que en Blazor
                var envioDto = new EnvioDto
                {
                    NumeroSeguimiento = NumeroSeguimiento,
                    FechaCreacionEnvio = DateTime.Now,
                    FechaSalida = FechaSalida,
                    Estado = EstadoEnvio,
                    PesoKg = PesoKg,
                    VolumenM3 = VolumenM3,
                    Descripcion = Descripcion,
                    CostoTotal = CostoTotal,
                    // Asignar los objetos completos desde las selecciones
                    Cliente = SelectedCliente!,
                    Conductor = SelectedConductor!,
                    Vehiculo = SelectedVehiculo!,
                    Origen = SelectedOrigen!,
                    Destino = SelectedDestino!,
                    TipoCarga = SelectedTipoCarga!
                };

                var result = await _envioService.CreateEnvioAsync(envioDto);

                if (result != null)
                {
                    ShowSuccessMessage("Envío creado exitosamente");
                    await Task.Delay(1500);
                    await Shell.Current.GoToAsync("///MainPage");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al crear envío: {ex.Message}");
                ShowErrorMessage($"Error al crear el envío: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(NumeroSeguimiento))
            {
                ShowErrorMessage("El número de seguimiento es requerido");
                return false;
            }

            if (SelectedCliente == null)
            {
                ShowErrorMessage("Debe seleccionar un cliente");
                return false;
            }

            if (SelectedConductor == null)
            {
                ShowErrorMessage("Debe seleccionar un conductor");
                return false;
            }

            if (SelectedVehiculo == null)
            {
                ShowErrorMessage("Debe seleccionar un vehículo");
                return false;
            }

            if (SelectedOrigen == null)
            {
                ShowErrorMessage("Debe seleccionar un origen");
                return false;
            }

            if (SelectedDestino == null)
            {
                ShowErrorMessage("Debe seleccionar un destino");
                return false;
            }

            if (SelectedOrigen.IdUbicacion == SelectedDestino.IdUbicacion)
            {
                ShowErrorMessage("El origen y destino no pueden ser iguales");
                return false;
            }

            if (SelectedTipoCarga == null)
            {
                ShowErrorMessage("Debe seleccionar un tipo de carga");
                return false;
            }

            if (PesoKg <= 0)
            {
                ShowErrorMessage("El peso debe ser mayor a 0");
                return false;
            }

            if (VolumenM3 <= 0)
            {
                ShowErrorMessage("El volumen debe ser mayor a 0");
                return false;
            }

            if (CostoTotal <= 0)
            {
                ShowErrorMessage("El costo total debe ser mayor a 0");
                return false;
            }

            if (FechaSalida < DateTime.Now.Date)
            {
                ShowErrorMessage("La fecha de salida no puede ser anterior a hoy");
                return false;
            }

            return true;
        }

        private void ShowSuccessMessage(string msg)
        {
            Message = msg;
            IsMessageError = false;
        }

        private async void ShowErrorMessage(string msg)
        {
            Message = msg;
            IsMessageError = true;
            await Task.Delay(3000);
            Message = "";
        }

        private async void Cancelar()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
