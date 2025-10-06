using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GestionLogisticaApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _bienvenidaMessage = "Bienvenido a";
        private string _tituloApp = "GESTION LOGISTICA";
        private string _descripcion = "Sistema de Gesti�n Log�stica\nInstituto Superior De Profesorado N20";
        private DateTime _fechaActual = DateTime.Now;

        public string BienvenidaMessage
        {
            get => _bienvenidaMessage;
            set { _bienvenidaMessage = value; OnPropertyChanged(); }
        }

        public string TituloApp
        {
            get => _tituloApp;
            set { _tituloApp = value; OnPropertyChanged(); }
        }

        public string Descripcion
        {
            get => _descripcion;
            set { _descripcion = value; OnPropertyChanged(); }
        }

        public DateTime FechaActual
        {
            get => _fechaActual;
            set { _fechaActual = value; OnPropertyChanged(); OnPropertyChanged(nameof(FechaFormateada)); }
        }

        public string FechaFormateada => FechaActual.ToString("dddd, dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));

        public ObservableCollection<AccesoRapidoItem> AccesosRapidos { get; set; } = new();

        public ICommand NavegaCommand { get; }

        public MainPageViewModel()
        {
            NavegaCommand = new Command<string>(OnNavegar);
            CargarAccesosRapidos();
            
            // Actualizar fecha cada minuto
            var timer = new Timer(ActualizarFecha, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private void CargarAccesosRapidos()
        {
            AccesosRapidos.Add(new AccesoRapidoItem
            {
                Titulo = "Buscar Envios",
                Descripcion = "Encuentra envios por n�mero de seguimiento",
                Ruta = "//EnviosPage",
                Color = "#2196F3"
            });

            AccesosRapidos.Add(new AccesoRapidoItem
            {
                Titulo = "Mis Envios",
                Descripcion = "Ver historial envios",
                //Ruta = "//EnviosPage",
                Color = "#FF9800"
            });
        }

        private async void OnNavegar(string ruta)
        {
            if (!string.IsNullOrEmpty(ruta))
            {
                await Shell.Current.GoToAsync(ruta);
            }
        }

        private void ActualizarFecha(object state)
        {
            FechaActual = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AccesoRapidoItem
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Ruta { get; set; } = string.Empty;
        public string Color { get; set; } = "#2196F3";
    }
}