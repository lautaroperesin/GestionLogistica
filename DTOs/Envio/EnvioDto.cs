using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.DTOs.Vehiculo;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.DTOs.Envio
{
    public class EnvioDto
    {
        public int IdEnvio { get; set; }
        public UbicacionDto Origen { get; set; } = new();
        public UbicacionDto Destino { get; set; } = new();
        public string? NumeroSeguimiento { get; set; }
        public DateTime FechaCreacionEnvio { get; set; }
        public DateTime? FechaSalida { get; set; }
        public EstadoEnvioDto Estado { get; set; } = new();
        public decimal PesoKg { get; set; }
        public decimal VolumenM3 { get; set; }
        public string? Descripcion { get; set; }
        public decimal CostoTotal { get; set; }
        public VehiculoDto Vehiculo { get; set; } = new();
        public ConductorDto Conductor { get; set; } = new();
        public ClienteDto Cliente { get; set; } = new();
        public TipoCargaDto TipoCarga { get; set; } = new();
    }
}
