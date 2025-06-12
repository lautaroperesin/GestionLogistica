using GestionLogisticaBackend.DTOs.Cliente;
using GestionLogisticaBackend.DTOs.Conductor;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Ubicacion;
using GestionLogisticaBackend.DTOs.Vehiculo;
using GestionLogisticaBackend.Services.Interfaces;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class EnvioService : IEnvioService
    {
        private readonly LogisticaContext _context;

        public EnvioService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<List<EnvioDto>> GetAllEnviosAsync()
        {
            var envios = await _context.Envios
                .Include(e => e.Cliente)
                .Include(e => e.Conductor)
                .Include(e => e.Vehiculo)
                .Include(e => e.Origen)
                .Include(e => e.Destino)
                .Include(e => e.TipoCarga)
                .Include(e => e.Estado)
                .ToListAsync();
            return envios.Select(e => new EnvioDto
            {
                IdEnvio = e.IdEnvio,
                Origen = new UbicacionDto
                {
                    IdUbicacion = e.Origen.IdUbicacion,
                    Direccion = e.Origen.Direccion
                },
                Destino = new UbicacionDto
                {
                    IdUbicacion = e.Destino.IdUbicacion,
                    Direccion = e.Destino.Direccion,
                },
                FechaCreacionEnvio = e.FechaCreacionEnvio,
                FechaSalidaProgramada = e.FechaSalidaProgramada,
                FechaSalidaReal = e.FechaSalidaReal,
                FechaEntregaEstimada = e.FechaEntregaEstimada,
                FechaEntregaReal = e.FechaEntregaReal,
                Estado = new EstadoEnvioDto
                {
                    Nombre = e.Estado.Nombre
                },
                PesoKg = e.PesoKg,
                Descripcion = e.Descripcion,
                CostoTotal = e.CostoTotal,
                Vehiculo = new VehiculoDto
                {
                    IdVehiculo = e.Vehiculo.IdVehiculo,
                    Patente = e.Vehiculo.Patente,
                    Marca = e.Vehiculo.Marca,
                    Modelo = e.Vehiculo.Modelo
                },
                Conductor = new ConductorDto
                {
                    IdConductor = e.Conductor.IdConductor,
                    Nombre = e.Conductor.Nombre,
                    Dni = e.Conductor.Dni,
                    Telefono = e.Conductor.Telefono
                },
                Cliente = new ClienteDto
                {
                    IdCliente = e.Cliente.IdCliente,
                    Nombre = e.Cliente.Nombre,
                    Email = e.Cliente.Email,
                    Telefono = e.Cliente.Telefono
                },
                TipoCarga = new TipoCargaDto
                {
                    Nombre = e.TipoCarga.Nombre
                }
            }).ToList();
        }

        public async Task<EnvioDto?> GetEnvioByIdAsync(int id)
        {
            var envio = await _context.Envios
                .Include(e => e.Cliente)
                .Include(e => e.Conductor)
                .Include(e => e.Vehiculo)
                .Include(e => e.Origen)
                .Include(e => e.Destino)
                .Include(e => e.TipoCarga)
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(e => e.IdEnvio == id);

            if (envio == null) return null;

            return new EnvioDto
            {
                IdEnvio = envio.IdEnvio,
                Origen = new UbicacionDto
                {
                    IdUbicacion = envio.Origen.IdUbicacion,
                    Direccion = envio.Origen.Direccion
                },
                Destino = new UbicacionDto
                {
                    IdUbicacion = envio.Destino.IdUbicacion,
                    Direccion = envio.Destino.Direccion,
                },
                FechaCreacionEnvio = envio.FechaCreacionEnvio,
                FechaSalidaProgramada = envio.FechaSalidaProgramada,
                FechaSalidaReal = envio.FechaSalidaReal,
                FechaEntregaEstimada = envio.FechaEntregaEstimada,
                FechaEntregaReal = envio.FechaEntregaReal,
                Estado = new EstadoEnvioDto
                {
                    Nombre = envio.Estado.Nombre
                },
                PesoKg = envio.PesoKg,
                Descripcion = envio.Descripcion,
                CostoTotal = envio.CostoTotal,
                Vehiculo = new VehiculoDto
                {
                    IdVehiculo = envio.Vehiculo.IdVehiculo,
                    Patente = envio.Vehiculo.Patente,
                    Marca = envio.Vehiculo.Marca,
                    Modelo = envio.Vehiculo.Modelo
                },
                Conductor = new ConductorDto
                {
                    IdConductor = envio.Conductor.IdConductor,
                    Nombre = envio.Conductor.Nombre,
                    Dni = envio.Conductor.Dni,
                    Telefono = envio.Conductor.Telefono
                },
                Cliente = new ClienteDto
                {
                    IdCliente = envio.Cliente.IdCliente,
                    Nombre = envio.Cliente.Nombre,
                    Email = envio.Cliente.Email,
                    Telefono = envio.Cliente.Telefono
                },
                TipoCarga = new TipoCargaDto
                {
                    Nombre = envio.TipoCarga.Nombre
                }
            };
        }

        public async Task<EnvioDto> CreateEnvioAsync(CreateEnvioDto envioDto)
        {
            var envio = new Envio
            {
                IdOrigen = envioDto.IdOrigen,
                IdDestino = envioDto.IdDestino,
                FechaCreacionEnvio = DateTime.Now,
                FechaSalidaProgramada = envioDto.FechaSalidaProgramada,
                FechaEntregaEstimada = envioDto.FechaEntregaEstimada,
                PesoKg = envioDto.PesoKg,
                Descripcion = envioDto.Descripcion,
                CostoTotal = envioDto.CostoTotal,
                IdVehiculo = envioDto.IdVehiculo,
                IdConductor = envioDto.IdConductor,
                IdCliente = envioDto.IdCliente,
                IdTipoCarga = envioDto.IdTipoCarga,
                IdEstado = 1
            };

            _context.Envios.Add(envio);

            await _context.SaveChangesAsync();

            return new EnvioDto
            {
                IdEnvio = envio.IdEnvio,
                Origen = new UbicacionDto { IdUbicacion = envio.IdOrigen },
                Destino = new UbicacionDto { IdUbicacion = envio.IdDestino },
                FechaCreacionEnvio = envio.FechaCreacionEnvio,
                FechaSalidaProgramada = envio.FechaSalidaProgramada,
                FechaEntregaEstimada = envio.FechaEntregaEstimada,
                PesoKg = envio.PesoKg,
                Descripcion = envio.Descripcion,
                CostoTotal = envio.CostoTotal,
                Vehiculo = new VehiculoDto { IdVehiculo = envio.IdVehiculo },
                Conductor = new ConductorDto { IdConductor = envio.IdConductor },
                Cliente = new ClienteDto { IdCliente = envio.IdCliente },
                TipoCarga = new TipoCargaDto { IdTipoCarga = envio.IdTipoCarga }
            };
        }

        public async Task<EnvioDto?> UpdateEnvioAsync(int id, UpdateEnvioDto envioDto)
        {
            var envio = await _context.Envios.FindAsync(id);

            if (envio == null)
            {
                return null;
            }

            envio.IdOrigen = envioDto.IdOrigen;
            envio.IdDestino = envioDto.IdDestino;
            envio.FechaSalidaProgramada = envioDto.FechaSalidaProgramada;
            envio.FechaEntregaEstimada = envioDto.FechaEntregaEstimada;
            envio.PesoKg = envioDto.PesoKg;
            envio.Descripcion = envioDto.Descripcion;
            envio.CostoTotal = envioDto.CostoTotal;
            envio.IdVehiculo = envioDto.IdVehiculo;
            envio.IdConductor = envioDto.IdConductor;
            envio.IdCliente = envioDto.IdCliente;
            envio.IdTipoCarga = envioDto.IdTipoCarga;
            envio.IdEstado = envioDto.IdEstado;
            envio.FechaSalidaReal = envioDto.FechaSalidaReal ?? envio.FechaSalidaReal;
            envio.FechaEntregaReal = envioDto.FechaEntregaReal ?? envio.FechaEntregaReal;

            await _context.SaveChangesAsync();

            return new EnvioDto
            {
                IdEnvio = envio.IdEnvio,
                Origen = new UbicacionDto { IdUbicacion = envio.IdOrigen },
                Destino = new UbicacionDto { IdUbicacion = envio.IdDestino },
                FechaCreacionEnvio = envio.FechaCreacionEnvio,
                FechaSalidaProgramada = envio.FechaSalidaProgramada,
                FechaEntregaEstimada = envio.FechaEntregaEstimada,
                FechaSalidaReal = envio.FechaSalidaReal,
                FechaEntregaReal = envio.FechaEntregaReal,
                Estado = new EstadoEnvioDto { IdEstado = envio.IdEstado },
                PesoKg = envio.PesoKg,
                Descripcion = envio.Descripcion,
                CostoTotal = envio.CostoTotal,
                Vehiculo = new VehiculoDto { IdVehiculo = envio.IdVehiculo },
                Conductor = new ConductorDto { IdConductor = envio.IdConductor },
                Cliente = new ClienteDto { IdCliente = envio.IdCliente },
                TipoCarga = new TipoCargaDto { IdTipoCarga = envio.IdTipoCarga }
            };
        }

        public async Task<bool> DeleteEnvioAsync(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null) return false;

            envio.Deleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
