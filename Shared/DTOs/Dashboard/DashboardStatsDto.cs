namespace GestionLogisticaBackend.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        // Envíos
        public int EnviosEnTransito { get; set; }
        public int EnviosEnPreparacion { get; set; }
        public int EnviosEntregados { get; set; }
        public int EnviosPendientes { get; set; }
        public int EnviosCancelados { get; set; } // Incluye cancelados, incidentes y demorados
        public int EnviosEsteMes { get; set; }

        // Clientes
        public int TotalClientes { get; set; }

        // Vehículos
        public int TotalVehiculos { get; set; }
    }

}
