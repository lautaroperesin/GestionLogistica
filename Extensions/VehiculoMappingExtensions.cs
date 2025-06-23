using GestionLogisticaBackend.DTOs.Vehiculo;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Extensions
{
    public static class VehiculoMappingExtensions
    {
        /// <summary>
        /// Mapea una entidad a un DTO.
        /// </summary>
        public static VehiculoDto ToDto(this Vehiculo vehiculo)
        {
            if (vehiculo == null) return null!;

            return new VehiculoDto
            {
                IdVehiculo = vehiculo.IdVehiculo,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Patente = vehiculo.Patente,
                CapacidadCarga = vehiculo.CapacidadKg,
                UltimaInspeccion = vehiculo.UltimaInspeccion
            };
        }

        /// <summary>
        /// Mapea una colección de entidad a una colección de DTO.
        /// </summary>
        public static IEnumerable<VehiculoDto> ToDtoList(this IEnumerable<Vehiculo> vehiculos)
        {
            return vehiculos.Select(c => c.ToDto());
        }

        /// <summary>
        /// Mapea un DTO a una entidad.
        /// </summary>
        public static Vehiculo ToEntity(this CreateVehiculoDto dto)
        {
            if (dto == null) return null!;

            return new Vehiculo
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Patente = dto.Patente,
                CapacidadKg = dto.CapacidadCarga,
                UltimaInspeccion = dto.UltimaInspeccion
            };
        }

        /// <summary>
        /// Actualiza una entidad existente con los datos del DTO.
        /// Útil para PUT o PATCH.
        /// </summary>
        public static void UpdateFromDto(this Vehiculo vehiculo, UpdateVehiculoDto dto)
        {
            if (vehiculo == null || dto == null) return;

            vehiculo.Marca = dto.Marca;
            vehiculo.Modelo = dto.Modelo;
            vehiculo.Patente = dto.Patente;
            vehiculo.CapacidadKg = dto.CapacidadCarga;
            vehiculo.UltimaInspeccion = dto.UltimaInspeccion;
        }
    }
}
