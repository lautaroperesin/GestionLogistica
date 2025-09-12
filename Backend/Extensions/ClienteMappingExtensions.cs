using GestionLogisticaBackend.DTOs.Cliente;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Extensions
{
    public static class ClienteMappingExtensions
    {
        /// <summary>
        /// Mapea una entidad a un DTO.
        /// </summary>
        public static ClienteDto ToDto(this Cliente cliente)
        {
            if (cliente == null) return null!;

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };
        }

        /// <summary>
        /// Mapea una colección de entidad a una colección de DTO.
        /// </summary>
        public static IEnumerable<ClienteDto> ToDtoList(this IEnumerable<Cliente> clientes)
        {
            return clientes.Select(c => c.ToDto());
        }

        /// <summary>
        /// Mapea un DTO a una entidad.
        /// </summary>
        public static Cliente ToEntity(this CreateClienteDto dto)
        {
            if (dto == null) return null!;

            return new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono ?? string.Empty
            };
        }

        /// <summary>
        /// Actualiza una entidad existente con los datos del DTO.
        /// Útil para PUT o PATCH.
        /// </summary>
        public static void UpdateFromDto(this Cliente cliente, UpdateClienteDto dto)
        {
            if (cliente == null || dto == null) return;

            cliente.Nombre = dto.Nombre;
            cliente.Email = dto.Email;
            cliente.Telefono = dto.Telefono ?? string.Empty;
        }
    }
}
