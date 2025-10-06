using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionLogisticaBackend.DTOs.Envio;
using Shared.ApiServices;
using GestionLogisticaBackend.DTOs.Filters;
using Microsoft.Maui.Storage;

namespace GestionLogisticaApp.ViewModels
{
    public partial class EnviosViewModel : ObservableObject
    {
        EnvioApiService _envioService;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string mensajeVacio = "No tienes envios activos";

        [ObservableProperty]
        private ObservableCollection<EnvioDto> envios = new();

        [ObservableProperty]
        private string searchNumero = string.Empty;

        [ObservableProperty]
        private string message = string.Empty;

        public IRelayCommand SearchCommand { get; }

        private int _idUserLogin;

        public EnviosViewModel()
        {
            _envioService = new EnvioApiService();
            SearchCommand = new RelayCommand(OnSearch);
            _idUserLogin = Preferences.Get("UserLoginId", 0);
        }

        //private bool CanSearch()
        //{
        //    return !IsBusy && !string.IsNullOrWhiteSpace(SearchNumero);
        //}

        private async void OnSearch()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                Envios.Clear();

                var filtros = new EnvioFilterDto
                {
                    NumeroSeguimiento = SearchNumero,
                    PageNumber = 1,
                    PageSize = 50
                };

                var result = await _envioService.GetEnviosAsync(filtros);
                if (result != null && result.Items != null)
                {
                    foreach (var e in result.Items)
                    {
                        Envios.Add(e);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar error (mostrar mensaje, logging, etc.)
                Message = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
