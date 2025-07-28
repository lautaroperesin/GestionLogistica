using GestionLogisticaBackend.DTOs.Dashboard;
using GestionLogisticaBackend.DTOs.Envio;
using GestionLogisticaBackend.DTOs.Factura;
using GestionLogisticaBackend.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionLogisticaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("estadisticas")]
        public async Task<ActionResult<DashboardStatsDto>> GetEstadisticas()
        {
            var estadisticas = await _dashboardService.GetDashboardStatsAsync();
            return Ok(estadisticas);
        }

        [HttpGet("envios-recientes")]
        public async Task<ActionResult<IEnumerable<EnvioRecienteDto>>> GetEnviosRecientes()
        {
            var enviosRecientes = await _dashboardService.GetEnviosRecientesAsync();
            return Ok(enviosRecientes);
        }
    }
}
