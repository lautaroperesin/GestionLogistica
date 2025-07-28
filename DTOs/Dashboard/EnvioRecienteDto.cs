namespace GestionLogisticaBackend.DTOs.Dashboard
{
    public class EnvioRecienteDto
    {
        public int Id { get; set; }
        public string NumeroSeguimiento { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }
}
