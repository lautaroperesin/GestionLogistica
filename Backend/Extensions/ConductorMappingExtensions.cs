using GestionLogisticaBackend.DTOs.Conductor;
using LogisticaBackend.Models;

namespace GestionLogisticaBackend.Extensions
{
    public static class ConductorMappingExtensions
    {
        /// <summary>
        /// Mapea una entidad a un DTO.
        /// </summary>
        public static ConductorDto ToDto(this Conductor conductor)
        {
            if (conductor == null) return null!;

            return new ConductorDto
            {
                IdConductor = conductor.IdConductor,
                Nombre = conductor.Nombre,
                Dni = conductor.Dni,
                Telefono = conductor.Telefono,
                Email = conductor.Email,
                ClaseLicencia = conductor.ClaseLicencia,
                VencimientoLicencia = conductor.VencimientoLicencia
            };
        }

        /// <summary>
        /// Mapea una colección de entidad a una colección de DTO.
        /// </summary>
        public static IEnumerable<ConductorDto> ToDtoList(this IEnumerable<Conductor> conductors)
        {
            return conductors.Select(c => c.ToDto());
        }

        /// <summary>
        /// Mapea un DTO a una entidad.
        /// </summary>
        public static Conductor ToEntity(this CreateConductorDto dto)
        {
            if (dto == null) return null!;

            return new Conductor
            {
                Nombre = dto.Nombre,
                Dni = dto.Dni,
                Telefono = dto.Telefono,
                Email = dto.Email,
                ClaseLicencia = dto.ClaseLicencia,
                VencimientoLicencia = dto.VencimientoLicencia
            };
        }

        /// <summary>
        /// Actualiza una entidad existente con los datos del DTO.
        /// Útil para PUT o PATCH.
        /// </summary>
        public static void UpdateFromDto(this Conductor conductor, UpdateConductorDto dto)
        {
            if (conductor == null) throw new ArgumentNullException(nameof(conductor));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            conductor.Nombre = dto.Nombre;
            conductor.Dni = dto.Dni;
            conductor.Telefono = dto.Telefono;
            conductor.Email = dto.Email;
            conductor.ClaseLicencia = dto.ClaseLicencia;
            conductor.VencimientoLicencia = dto.VencimientoLicencia;
        }
    }
}
