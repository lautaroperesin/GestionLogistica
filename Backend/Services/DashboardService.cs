using GestionLogisticaBackend.DTOs.Dashboard;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Extensions;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Implementations
{
    public class DashboardService
    {
        private readonly LogisticaContext _context;

        public DashboardService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<object> GetDashboardStatsAsync()
        {
            var hoy = DateTime.Today;
            var mesActual = hoy.Month;
            var anioActual = hoy.Year;
            var mesAnterior = hoy.AddMonths(-1).Month;
            var anioMesAnterior = hoy.AddMonths(-1).Year;

            // Total de envíos del mes actual
            var totalEnviosMes = await _context.Envios
                .CountAsync(e => e.FechaSalida.Value.Month == mesActual && e.FechaSalida.Value.Year == anioActual);

            // En tránsito
            var enTransito = await _context.Envios.CountAsync(e => e.Estado == EstadoEnvioEnum.EnTransito);

            // Entregados
            var entregados = await _context.Envios.CountAsync(e => e.Estado == EstadoEnvioEnum.Entregado);

            // Envíos pendientes
            var pendientes = await _context.Envios.CountAsync(e => e.Estado == EstadoEnvioEnum.Pendiente);

            // cancelados/incidentes/demorados
            var cancelados = await _context.Envios.CountAsync(e => e.Estado == EstadoEnvioEnum.None || e.Estado == EstadoEnvioEnum.Demorado || e.Estado == EstadoEnvioEnum.Cancelado);

            // Clientes totales
            var totalClientes = await _context.Clientes.CountAsync();

            // Flota total
            var flotaTotal = await _context.Vehiculos.CountAsync();

            return new DashboardStatsDto
            {
                EnviosEnTransito = enTransito,
                EnviosEntregados = entregados,
                EnviosPendientes = pendientes,
                EnviosCancelados = cancelados,
                EnviosEsteMes = totalEnviosMes,
                TotalClientes = totalClientes,
                TotalVehiculos = flotaTotal,
            };
        }

        // obtener envios recientes
        public async Task<IEnumerable<EnvioRecienteDto>> GetEnviosRecientesAsync(int cantidad = 5)
        {
            var enviosRecientes = await _context.Envios
                .Include(e => e.Origen)
                .ThenInclude(e => e.Localidad)
                .Include(e => e.Destino)
                .ThenInclude(e => e.Localidad)
                .Include(e => e.Estado)
                .Include(e => e.Cliente)
                .OrderByDescending(e => e.FechaCreacionEnvio)
                .Take(cantidad)
                .ToListAsync();

            var enviosRecientesDto = enviosRecientes.Select(e => new EnvioRecienteDto
            {
                Id = e.IdEnvio,
                FechaCreacion = e.FechaCreacionEnvio,
                NumeroSeguimiento = e.NumeroSeguimiento,
                Cliente = e.Cliente.Nombre,
                Origen = e.Origen.Localidad.Nombre,
                Destino = e.Destino.Localidad.Nombre,
                Estado = e.Estado.ToString(),
            });

            return enviosRecientesDto;
        }
    }
}
