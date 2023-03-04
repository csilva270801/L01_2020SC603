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
            List<pedidos> listadoPedidos = (from e in _restauranteContexto.pedidos
                                            select e).ToList(); 
            if (listadoPedidos.Count == 0) 
            {
                return NotFound();
            } 

            return Ok(listadoPedidos);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedidos([FromBody]pedidos pedidos)
        {
            try
            {
                _restauranteContexto.pedidos.Add(pedidos);
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
            pedidos? pedidos = (from e in _restauranteContexto.pedidos
                                where e.pedidoId == id
                                select e).FirstOrDefault();
            if (pedidos == null)
                return NotFound();

            _restauranteContexto.pedidos.Attach(pedidos);
            _restauranteContexto.pedidos.Remove(pedidos);
            _restauranteContexto.SaveChanges();

            return Ok(pedidos);
        }

        [HttpPut]
        [Route("actualizar")]

    }
}
