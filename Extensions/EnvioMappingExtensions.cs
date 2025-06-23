using GestionLogisticaBackend.DTOs.Envio;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Extensions
{
    public static class EnvioMappingExtensions
    {
        /// <summary>
        /// Mapea una entidad a un DTO.
        /// </summary>
        public static EnvioDto ToDto(this Envio envio)
        {
            if (envio == null) return null!;

            return new EnvioDto
            {
                IdEnvio = envio.IdEnvio,
                FechaCreacionEnvio = envio.FechaCreacionEnvio,
                FechaSalidaProgramada = envio.FechaSalidaProgramada,
                FechaSalidaReal = envio.FechaSalidaReal,
                FechaEntregaEstimada = envio.FechaEntregaEstimada,
                FechaEntregaReal = envio.FechaEntregaReal,
                PesoKg = envio.PesoKg,
                Descripcion = envio.Descripcion,
                CostoTotal = envio.CostoTotal,

                Origen = envio.Origen.ToDto(),
                Destino = envio.Destino.ToDto(),
                //Estado = envio.Estado.ToDto(),
                Vehiculo = envio.Vehiculo.ToDto(),
                Conductor = envio.Conductor.ToDto(),
                Cliente = envio.Cliente.ToDto(),
                //TipoCarga = envio.TipoCarga.ToDto()
            };
        }

        /// <summary>
        /// Mapea una colección de entidad a una colección de DTO.
        /// </summary>
        public static IEnumerable<EnvioDto> ToDtoList(this IEnumerable<Envio> envios)
        {
            return envios.Select(c => c.ToDto());
        }

        /// <summary>
        /// Mapea un DTO a una entidad.
        /// </summary>
        public static Envio ToEntity(this CreateEnvioDto dto)
        {
            if (dto == null) return null!;

            return new Envio
            {
                FechaCreacionEnvio = DateTime.Now,
                FechaSalidaProgramada = dto.FechaSalidaProgramada,
                FechaEntregaEstimada = dto.FechaEntregaEstimada,
                PesoKg = dto.PesoKg,
                Descripcion = dto.Descripcion,
                CostoTotal = dto.CostoTotal,

                IdOrigen = dto.IdOrigen,
                IdDestino = dto.IdDestino,
                IdEstado = 1,   // VERIFICAR EL ESTADO DE ENVIO
                IdVehiculo = dto.IdVehiculo,
                IdConductor = dto.IdConductor,
                IdCliente = dto.IdCliente,
                IdTipoCarga = dto.IdTipoCarga
            };
        }

        /// <summary>
        /// Actualiza una entidad existente con los datos del DTO.
        /// Útil para PUT o PATCH.
        /// </summary>
        public static void UpdateFromDto(this Envio envio, UpdateEnvioDto dto)
        {
            if (envio == null || dto == null) return;

            envio.FechaSalidaProgramada = dto.FechaSalidaProgramada;
            envio.FechaSalidaReal = dto.FechaSalidaReal;
            envio.FechaEntregaEstimada = dto.FechaEntregaEstimada;
            envio.FechaEntregaReal = dto.FechaEntregaReal;
            envio.PesoKg = dto.PesoKg;
            envio.Descripcion = dto.Descripcion;
            envio.CostoTotal = dto.CostoTotal;

            envio.IdOrigen = dto.IdOrigen;
            envio.IdDestino = dto.IdDestino;
            envio.IdEstado = dto.IdEstado;
            envio.IdVehiculo = dto.IdVehiculo;
            envio.IdConductor = dto.IdConductor;
            envio.IdCliente = dto.IdCliente;
            envio.IdTipoCarga = dto.IdTipoCarga;
        }
    }
}
