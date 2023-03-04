using System.ComponentModel.DataAnnotations;

namespace L01_2020SC603.Models
{
    public class clientes
    {
        public string? nombreCliente { get; set; }
        public string? direccion { get; set; }
    }

    public class pedidos
    {
        public int? pedidoId { get; set; }
        public int? clienteId { get; set; }
        public int? platoId { get; set; }
        public int? cantidad { get; set; }
        public decimal? precio { get; set; }
    }

    public class platos 
    {
        public string? nombrePlato { get; set; }

        public decimal? precio { get; set; }
    }
}
