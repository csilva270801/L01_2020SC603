using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020SC603.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020SC603.Controllers
{
    public class pedidosController : Controller
    {
        private readonly restauranteContext _restauranteContexto;

        public pedidosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Pedidos> listadoPedidos = (from e in _restauranteContexto.Pedidos
                                            select e).ToList(); 
            if (listadoPedidos.Count == 0) 
            {
                return NotFound();
            } 

            return Ok(listadoPedidos);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedidos([FromBody]Pedidos pedidos)
        {
            try
            {
                _restauranteContexto.Pedidos.Add(pedidos);
                _restauranteContexto.SaveChanges();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarPedidos(int id) 
        {
            Pedidos? pedidos = (from e in _restauranteContexto.Pedidos
                                where e.pedidoId == id
                                select e).FirstOrDefault();
            if (pedidos == null)
                return NotFound();

            _restauranteContexto.Pedidos.Attach(pedidos);
            _restauranteContexto.Pedidos.Remove(pedidos);
            _restauranteContexto.SaveChanges();

            return Ok(pedidos);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPedidos(int id, [FromBody] Pedidos pedidoModificar)
        {
            Pedidos? pedidoActual = (from e in _restauranteContexto.Pedidos
                                     where e.clienteId == id
                                     select e).FirstOrDefault();
            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(pedidoActual);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindyByDescription(int filtro)
        {
            Pedidos? pedido = (from e in _restauranteContexto.Pedidos
                               where e.clienteId == filtro
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/{cliente}")]
        public IActionResult FindByPedido(int cliente)
        {
            Pedidos? pedido = (from e in _restauranteContexto.Pedidos
                               where e.clienteId == cliente
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/{motorista}")]
        public IActionResult FindByMotorista(int motorista)
        {
            Pedidos? pedido = (from e in _restauranteContexto.Pedidos
                               where e.motoristaId == motorista
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

    }
}
