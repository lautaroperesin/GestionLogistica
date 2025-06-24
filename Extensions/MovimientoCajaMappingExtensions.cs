using GestionLogisticaBackend.DTOs.MovimientoCaja;
using GestionLogisticaBackend.Models;

namespace GestionLogisticaBackend.Extensions
{
    public static class MovimientoCajaMappingExtensions
    {
        /// <summary>
        /// Mapea una entidad a un DTO.
        /// </summary>
        public static MovimientoCajaDto ToDto(this MovimientoCaja movimiento)
        {
            if (movimiento == null) return null!;

            return new MovimientoCajaDto
            {
                IdMovimiento = movimiento.IdMovimiento,
                Factura = movimiento.Factura.ToDto(),
                FechaPago = movimiento.FechaPago,
                Monto = movimiento.Monto,
                IdMetodoPago = movimiento.IdMetodoPago,
                Observaciones = movimiento.Observaciones
            };
        }

        /// <summary>
        /// Mapea una colección de entidad a una colección de DTO.
        /// </summary>
        public static IEnumerable<MovimientoCajaDto> ToDtoList(this IEnumerable<MovimientoCaja> movimientos)
        {
            return movimientos.Select(c => c.ToDto());
        }

        /// <summary>
        /// Mapea un DTO a una entidad.
        /// </summary>
        public static MovimientoCaja ToEntity(this CreateMovimientoCajaDto dto)
        {
            if (dto == null) return null!;

            return new MovimientoCaja
            {
                IdFactura = dto.IdFactura,
                FechaPago = dto.FechaPago,
                Monto = dto.Monto,
                IdMetodoPago = dto.IdMetodoPago,
                Observaciones = dto.Observaciones
            };
        }

        /// <summary>
        /// Actualiza una entidad existente con los datos del DTO.
        /// Útil para PUT o PATCH.
        /// </summary>
        //public static void UpdateFromDto(this MovimientoCaja movimiento, UpdateMovimientoCajaDto dto)
        //{
        //    if (movimiento == null || dto == null) return;

        //    movimiento.Nombre = dto.Nombre;
        //    movimiento.Email = dto.Email;
        //    movimiento.Telefono = dto.Telefono ?? string.Empty;
        //}
    }
}
