using GestionLogisticaBackend.DTOs.Dashboard;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.Enums;
using GestionLogisticaBackend.Extensions;
using LogisticaBackend.Data;
using LogisticaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLogisticaBackend.Services.Implementations
{
    public class DashboardService
    {
        private readonly LogisticaContext _context;

        public DashboardService(LogisticaContext context)
        {
            _context = context;
        }

        public async Task<Object> GetDashboardStatsAsync()
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
            var enTransito = await _context.Envios.CountAsync(e => e.IdEstado == 3);

            // Entregados
            var entregados = await _context.Envios.CountAsync(e => e.IdEstado == 4);

            // Envíos pendientes
            var pendientes = await _context.Envios.CountAsync(e => e.IdEstado == 1);

            // En preparacion
            var enPreparacion = await _context.Envios.CountAsync(e => e.IdEstado == 2);

            // cancelados/incidentes/demorados
            var cancelados = await _context.Envios.CountAsync(e => e.IdEstado == 5 || e.IdEstado == 6 || e.IdEstado == 7);

            // Ingresos del mes actual (ej: facturas cobradas - movimientos realizados)
            var ingresosMes = await _context.MovimientosCaja
                .Where(f => f.FechaPago.Month == mesActual && f.FechaPago.Year == anioActual)
                .SumAsync(f => (decimal?)f.Monto) ?? 0;

            var ingresosMesAnterior = await _context.MovimientosCaja
                .Where(f => f.FechaPago.Month == mesAnterior && f.FechaPago.Year == anioMesAnterior)
                .SumAsync(f => (decimal?)f.Monto) ?? 0;

            // facturacion pendiente
            var facturacionPendiente = await _context.Facturas
                .Where(f => f.Estado == EstadoFactura.Emitida || f.Estado == EstadoFactura.ParcialmentePagada)
                    .Select(f => new
                    {
                        f.Total,
                        MontoPagado = f.MovimientosCaja
                        .Sum(m => (decimal?)m.Monto) ?? 0
                    })
                    .SumAsync(x => x.Total - x.MontoPagado);

            // Clientes totales
            var totalClientes = await _context.Clientes.CountAsync();

            // Flota activa vs total
            var flotaDisponible = await _context.Vehiculos.CountAsync(v => v.Estado == EstadoVehiculo.Disponible);
            var flotaTotal = await _context.Vehiculos.CountAsync();
            var flotaEnMantenimiento = await _context.Vehiculos.CountAsync(v => v.Estado == EstadoVehiculo.EnMantenimiento);

            return new DashboardStatsDto
            {
                EnviosEnTransito = enTransito,
                EnviosEntregados = entregados,
                EnviosPendientes = pendientes,
                EnviosEnPreparacion = enPreparacion,
                EnviosCancelados = cancelados,
                EnviosEsteMes = totalEnviosMes,
                IngresosEsteMes = ingresosMes,
                IngresosMesAnterior = ingresosMesAnterior,
                FacturacionPendiente = facturacionPendiente,
                TotalClientes = totalClientes,
                TotalVehiculos = flotaTotal,
                VehiculosActivos = flotaDisponible,
                VehiculosEnMantenimiento = flotaEnMantenimiento
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
                Estado = e.Estado.Nombre,
            });

            return enviosRecientesDto;
        }
    }
}
