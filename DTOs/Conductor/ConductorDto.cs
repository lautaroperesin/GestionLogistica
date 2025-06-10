namespace GestionLogisticaBackend.DTOs.Conductor
{
    public class ConductorDto
    {
        public int IdConductor { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string ClaseLicencia { get; set; }
        public DateTime VencimientoLicencia { get; set; }
    }
}
