namespace GestionLogisticaBackend.DTOs.Conductor
{
    public class ConductorDto
    {
        public int IdConductor { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string ClaseLicencia { get; set; } = string.Empty;
        public DateTime VencimientoLicencia { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool LicenciaVencida => VencimientoLicencia < DateTime.Now;
    }
}
