using GestionLogisticaBackend.DTOs.Ubicacion;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Extensions
{
    public static class UbicacionMappingExtensions
    {
        /// <summary>
        /// Mapea una entidad a un DTO.
        /// </summary>
        public static UbicacionDto ToDto(this Ubicacion ubicacion)
        {
            if (ubicacion == null) return null!;

            return new UbicacionDto
            {
                IdUbicacion = ubicacion.IdUbicacion,
                Direccion = ubicacion.Direccion,
                Localidad = new LocalidadDto
                {
                    IdLocalidad = ubicacion.Localidad.IdLocalidad,
                    Nombre = ubicacion.Localidad.Nombre,
                    Provincia = new ProvinciaDto
                    {
                        IdProvincia = ubicacion.Localidad.Provincia.IdProvincia,
                        Nombre = ubicacion.Localidad.Provincia.Nombre,
                        Pais = new PaisDto
                        {
                            IdPais = ubicacion.Localidad.Provincia.Pais.IdPais,
                            Nombre = ubicacion.Localidad.Provincia.Pais.Nombre
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Mapea una colección de entidad a una colección de DTO.
        /// </summary>
        public static IEnumerable<UbicacionDto> ToDtoList(this IEnumerable<Ubicacion> ubicaciones)
        {
            return ubicaciones.Select(c => c.ToDto());
        }

        /// <summary>
        /// Mapea un DTO a una entidad.
        /// </summary>
        public static Ubicacion ToEntity(this CreateUbicacionDto dto)
        {
            if (dto == null) return null!;

            return new Ubicacion
            {
                Direccion = dto.Direccion,
                IdLocalidad = dto.IdLocalidad
            };
        }
    }
}
