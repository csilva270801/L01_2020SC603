using System.ComponentModel.DataAnnotations;

namespace L01_2020SC603.Models
{
    public class Clientes
    {
        [Key]
        public int? clienteId { get; set; }
        public string? nombreCliente { get; set; }
        public string? direccion { get; set; }
    }

}
