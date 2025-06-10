namespace GestionLogisticaBackend.DTOs.Cliente
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
    }
}
