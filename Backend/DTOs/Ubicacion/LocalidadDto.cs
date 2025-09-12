namespace GestionLogisticaBackend.DTOs.Ubicacion
{
    public class LocalidadDto
    {
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ProvinciaDto Provincia { get; set; } = new();
    }
}
