namespace GestionLogisticaBackend.DTOs.Ubicacion
{
    public class CreateUbicacionDto
    {
        public string Direccion { get; set; } = string.Empty;
        public int IdLocalidad { get; set; }
    }
}
