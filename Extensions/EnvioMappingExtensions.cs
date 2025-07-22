using GestionLogisticaBackend.DTOs.Envio;
using Humanizer;
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
            if (envio == null)
                throw new ArgumentNullException(nameof(envio));

            return new EnvioDto
            {
                IdEnvio = envio.IdEnvio,
                NumeroSeguimiento = envio.NumeroSeguimiento,
                FechaCreacionEnvio = envio.FechaCreacionEnvio,
                FechaSalida = envio.FechaSalida,
                PesoKg = envio.PesoKg,
                VolumenM3 = envio.VolumenM3,
                Descripcion = envio.Descripcion,
                CostoTotal = envio.CostoTotal,
                Origen = envio.Origen.ToDto(),
                Destino = envio.Destino.ToDto(),
                Estado = new EstadoEnvioDto
                {
                    IdEstado = envio.Estado.IdEstado,
                    Nombre = envio.Estado.Nombre
                },
                Vehiculo = envio.Vehiculo.ToDto(),
                Conductor = envio.Conductor.ToDto(),
                Cliente = envio.Cliente.ToDto(),
                TipoCarga = new TipoCargaDto
                {
                    IdTipoCarga = envio.IdTipoCarga,
                    Nombre = envio.TipoCarga.Nombre
                }
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
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Envio
            {
                FechaCreacionEnvio = DateTime.Now,
                NumeroSeguimiento = dto.NumeroSeguimiento,
                FechaSalida = dto.FechaSalida,
                PesoKg = dto.PesoKg,
                VolumenM3 = dto.VolumenM3,
                Descripcion = dto.Descripcion,
                CostoTotal = dto.CostoTotal,
                IdOrigen = dto.IdOrigen,
                IdDestino = dto.IdDestino,
                IdEstado = 1,
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

            envio.FechaSalida = dto.FechaSalida;
            envio.PesoKg = dto.PesoKg;
            envio.VolumenM3 = dto.VolumenM3;
            envio.Descripcion = dto.Descripcion;
            envio.CostoTotal = dto.CostoTotal;
            envio.NumeroSeguimiento = dto.NumeroSeguimiento;

            envio.IdOrigen = dto.IdOrigen;
            envio.IdDestino = dto.IdDestino;
            envio.IdVehiculo = dto.IdVehiculo;
            envio.IdConductor = dto.IdConductor;
            envio.IdCliente = dto.IdCliente;
            envio.IdTipoCarga = dto.IdTipoCarga;
        }
    }
}
