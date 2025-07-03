using GestionLogisticaBackend.DTOs.Factura;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Extensions
{
    public static class FacturaMappingExtensions
    {
        /// <summary>
        /// Mapea una entidad a un DTO.
        /// </summary>
        public static FacturaDto ToDto(this Factura factura)
        {
            if (factura == null) return null!;

            return new FacturaDto
            {
                IdFactura = factura.IdFactura,
                Envio = factura.Envio.ToDto(),
                Cliente = factura.Cliente.ToDto(),
                NumeroFactura = factura.NumeroFactura,
                FechaEmision = factura.FechaEmision,
                FechaVencimiento = factura.FechaVencimiento,
                Subtotal = factura.Subtotal,
                Iva = factura.Iva,
                Total = factura.Total,
                Estado = factura.Estado
            };
        }

        /// <summary>
        /// Mapea una colección de entidad a una colección de DTO.
        /// </summary>
        public static IEnumerable<FacturaDto> ToDtoList(this IEnumerable<Factura> facturas)
        {
            return facturas.Select(c => c.ToDto());
        }

        /// <summary>
        /// Mapea un DTO a una entidad.
        /// </summary>
        public static Factura ToEntity(this CreateFacturaDto dto)
        {
            if (dto == null) return null!;

            return new Factura
            {
                IdEnvio = dto.IdEnvio,
                IdCliente = dto.IdCliente,
                NumeroFactura = dto.NumeroFactura,
                FechaEmision = dto.FechaEmision,
                FechaVencimiento = dto.FechaVencimiento,
                Estado = dto.Estado,
                Subtotal = dto.Subtotal,
                Iva = dto.Iva,
                Total = dto.Total
            };
        }

        /// <summary>
        /// Actualiza una entidad existente con los datos del DTO.
        /// Útil para PUT o PATCH.
        /// </summary>
        public static void UpdateFromDto(this Factura factura, UpdateFacturaDto dto)
        {
            if (factura == null || dto == null) return;

            factura.IdEnvio = dto.IdEnvio;
            factura.IdCliente = dto.IdCliente;
            factura.NumeroFactura = dto.NumeroFactura;
            factura.FechaEmision = dto.FechaEmision;
            factura.FechaVencimiento = dto.FechaVencimiento;
            factura.Estado = dto.Estado;
            factura.Subtotal = dto.Subtotal;
            factura.Iva = dto.Iva;
            factura.Total = dto.Total;
        }
    }
}
