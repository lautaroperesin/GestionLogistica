using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GestionLogisticaBackend.Enums
{
    public enum EstadoFactura
    {
        Borrador = 0,
        Emitida = 1,
        ParcialmentePagada = 2,
        Pagada = 3,
        Vencida = 4,
        Anulada = 5
    }
}
