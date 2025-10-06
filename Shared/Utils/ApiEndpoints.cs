using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utils
{
    public static class ApiEndpoints
    {
        public static string Cliente { get; set; } = "clientes";
        public static string Conductor { get; set; } = "conductores";
        public static string Vehiculo { get; set; } = "vehiculos";
        public static string Usuario { get; set; } = "usuarios";
        public static string Login { get; set; } = "auth";
        public static string Envio { get; set; } = "envios";
        public static string Factura { get; set; } = "facturas";
        public static string Gemini { get; set; } = "gemini";
        public static string MovimientoCaja { get; set; } = "movimientoscaja";
        public static string TipoCarga { get; set; } = "tiposcarga";
        public static string Ubicacion { get; set; } = "ubicaciones";

        public static string GetEndpoint(string name)
        {
            return name switch
            {
                nameof(Cliente) => Cliente,
                nameof(Conductor) => Conductor,
                nameof(Vehiculo) => Vehiculo,
                nameof(Usuario) => Usuario,
                nameof(Login) => Login,
                nameof(Envio) => Envio,
                nameof(Factura) => Factura,
                nameof(Gemini) => Gemini,
                nameof(MovimientoCaja) => MovimientoCaja,
                nameof(TipoCarga) => TipoCarga, 
                nameof(Ubicacion) => Ubicacion,
                _ => throw new ArgumentException($"Endpoint '{name}' no está definido.")
            };
        }
    }
}
