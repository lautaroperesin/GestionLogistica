namespace GestionLogisticaBackend.DTOs.Ubicacion
{
    public class UbicacionDto
    {
        public int IdUbicacion { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public int IdLocalidad { get; set; }
        public string LocalidadNombre { get; set; } = string.Empty;
    }
}
