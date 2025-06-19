using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GestionLogisticaBackend.Enums
{
    public enum EstadoFactura
    {
        Borrador = 0,
        Emitida = 1,
        [Display(Name = "Parcialmente Pagada")]
        ParcialmentePagada = 2,
        Pagada = 3,
        Vencida = 4,
        Anulada = 5
    }

    public class EstadoFacturaHelper
    {
        /// <summary>
        /// Obtiene el nombre legible del estado de la factura.
        /// </summary>
        /// <param name="estado">El estado de la factura.</param>
        /// <returns>El nombre legible del estado.</returns>
        public static string ObtenerNombreEstado(EstadoFactura estado)
        {
            var display = estado.GetType()
                                .GetMember(estado.ToString())
                                .First()
                                .GetCustomAttribute<DisplayAttribute>();

            return display?.Name ?? estado.ToString();
        }
    }
}
