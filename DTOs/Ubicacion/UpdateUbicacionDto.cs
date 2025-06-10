namespace GestionLogisticaBackend.DTOs.Ubicacion
{
    public class UpdateUbicacionDto
    {
        public int IdUbicacion { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public int IdLocalidad { get; set; }
    }
}
