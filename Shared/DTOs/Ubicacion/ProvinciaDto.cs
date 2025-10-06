namespace GestionLogisticaBackend.DTOs.Ubicacion
{
    public class ProvinciaDto
    {
        public int IdProvincia { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public PaisDto Pais { get; set; } = new();
    }
}
