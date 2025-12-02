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
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;
using Service.Enums;
using GestionLogisticaBackend.DTOs.Usuario;

namespace GestionLogisticaApp.ViewModels
{
    public partial class EnviosPorConductorViewModel : ObservableObject
    {
        private readonly EnvioApiService _envioService;
        private readonly UsuarioApiService _usuarioService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private UsuarioDto? currentUser;

        [ObservableProperty]
        private string errorMessage = string.Empty;

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

        public IRelayCommand<EnvioDto> ViewDetalleCommand { get; }
        public IRelayCommand GoBackCommand { get; }
        public IRelayCommand RefreshEnviosCommand { get; }

        public EnviosPorConductorViewModel()
        {
            Debug.Print("EnviosPorConductorViewModel created");
            _envioService = new EnvioApiService();
            _usuarioService = new UsuarioApiService();

            ViewDetalleCommand = new RelayCommand<EnvioDto>(ViewDetalle);
            GoBackCommand = new RelayCommand(GoBack);
            RefreshEnviosCommand = new RelayCommand(async () => await LoadCurrentUserAndEnviosAsync());
            
            WeakReferenceMessenger.Default.Register<EnvioUpdatedMessage>(this, async (r, m) => await LoadEnviosAsync());
        }

        // Este método será llamado desde la página cuando aparezca
        public async Task InitializeAsync()
        {
            await LoadCurrentUserAndEnviosAsync();
        }

        private async Task LoadCurrentUserAndEnviosAsync()
        {
            if (IsBusy) return;
            
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                // Limpiar datos anteriores
                CurrentUser = null;
                EnviosPendientes.Clear();
                EnviosEnTransito.Clear();
                EnviosEntregados.Clear();
                EnviosDemorados.Clear();
                EnviosCancelados.Clear();

                // Obtener el ID del usuario logueado desde Preferences
                var userLoginId = Preferences.Get("UserLoginId", 0);
                Debug.WriteLine($"[EnviosPorConductor] UserLoginId desde Preferences: {userLoginId}");
                
                if (userLoginId == 0)
                {
                    ErrorMessage = "No hay un usuario logueado. Por favor, inicie sesión.";
                    Debug.WriteLine("[EnviosPorConductor] No hay usuario logueado");
                    return;
                }

                // Obtener la información del usuario
                CurrentUser = await _usuarioService.GetByIdAsync(userLoginId);
                Debug.WriteLine($"[EnviosPorConductor] Usuario obtenido: {CurrentUser?.Email}, ID: {CurrentUser?.Id}, Rol: {CurrentUser?.TipoRol}");
                
                if (CurrentUser == null)
                {
                    ErrorMessage = "No se pudo obtener la información del usuario.";
                    Debug.WriteLine("[EnviosPorConductor] CurrentUser es null");
                    return;
                }

                // Verificar que el usuario sea conductor
                if (CurrentUser.TipoRol != TipoRolEnum.Conductor)
                {
                    ErrorMessage = "Esta sección es solo para conductores. Tu rol no tiene acceso.";
                    Debug.WriteLine($"[EnviosPorConductor] Usuario no es conductor. Rol: {CurrentUser.TipoRol}");
                    return;
                }

                Debug.WriteLine("[EnviosPorConductor] Usuario es conductor, cargando envíos...");
                // Cargar los envíos del conductor
                await LoadEnviosAsync();
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"Error al cargar los datos: {ex.Message}";
                Debug.WriteLine($"[EnviosPorConductor] Exception: {ex.Message}");
                Debug.WriteLine($"[EnviosPorConductor] StackTrace: {ex.StackTrace}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadEnviosAsync()
        {
            //if (IsBusy) return;
            if (CurrentUser == null) return;

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
                    IdUsuario = CurrentUser.Id,
                    PageNumber = 1,
                    PageSize = 1000
                };

                Debug.WriteLine($"[EnviosPorConductor] Consultando envíos con IdUsuario: {filtros.IdUsuario}");
                
                var result = await _envioService.GetEnviosAsync(filtros);
                
                Debug.WriteLine($"[EnviosPorConductor] Resultado obtenido. Items: {result?.Items?.Count() ?? 0}, TotalItems: {result?.TotalItems ?? 0}");
                
                if (result != null && result.Items != null)
                {
                    var envios = result.Items.ToList();
                    Debug.WriteLine($"[EnviosPorConductor] Procesando {envios.Count} envíos");

                    foreach (var e in envios)
                    {
                        Debug.WriteLine($"[EnviosPorConductor] Envío {e.NumeroSeguimiento} - Estado: {e.Estado}");
                        
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
                                Debug.WriteLine($"[EnviosPorConductor] Envío ignorado por estado: {e.Estado}");
                                break;
                        }
                    }
                    
                    Debug.WriteLine($"[EnviosPorConductor] Resumen - Pendientes: {EnviosPendientes.Count}, EnTransito: {EnviosEnTransito.Count}, Entregados: {EnviosEntregados.Count}, Demorados: {EnviosDemorados.Count}, Cancelados: {EnviosCancelados.Count}");
                }
                else
                {
                    Debug.WriteLine("[EnviosPorConductor] Result es null o no tiene items");
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"Error al cargar los envíos: {ex.Message}";
                Debug.WriteLine($"[EnviosPorConductor] Exception en LoadEnviosAsync: {ex.Message}");
                Debug.WriteLine($"[EnviosPorConductor] StackTrace: {ex.StackTrace}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ViewDetalle(EnvioDto envio)
        {
            await Shell.Current.GoToAsync($"EnvioDetallePage?envioId={envio.IdEnvio}");
        }

        private async void GoBack()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
