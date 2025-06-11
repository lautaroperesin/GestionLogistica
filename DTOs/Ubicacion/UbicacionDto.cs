namespace GestionLogisticaBackend.DTOs.Ubicacion
{
    public class UbicacionDto
    {
        public int IdUbicacion { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public LocalidadDto Localidad { get; set; } = new();
        public string DireccionCompleta => $"{Direccion}, {Localidad.Nombre}, {Localidad.Provincia.Nombre}, {Localidad.Provincia.Pais.Nombre}";
    }
}
