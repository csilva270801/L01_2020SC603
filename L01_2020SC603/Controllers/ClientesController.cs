using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020SC603.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020SC603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public ClientesController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Clientes> listadoClientes = (from e in _restauranteContexto.Clientes
                                          select e).ToList();
            if (listadoClientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoClientes);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarClientes([FromBody] Clientes clientes)
        {
            try
            {
                _restauranteContexto.Clientes.Add(clientes);
                _restauranteContexto.SaveChanges();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarClientes(int  clienteId)
        {
            Clientes? clientes = (from e in _restauranteContexto.Clientes
                              where e.clienteId == clienteId
                              select e).FirstOrDefault();
            if (clientes == null)
                return NotFound();

            _restauranteContexto.Clientes.Attach(clientes);
            _restauranteContexto.Clientes.Remove(clientes);
            _restauranteContexto.SaveChanges();

            return Ok(clientes);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarCliente(int id, [FromBody] Clientes clientesModificar)
        {
            Clientes? clientesActual = (from e in _restauranteContexto.Clientes
                                    where e.clienteId == id
                                    select e).FirstOrDefault();
            if (clientesActual == null)
            {
                return NotFound();
            }

            clientesActual.nombreCliente = clientesModificar.nombreCliente;
            clientesActual.direccion = clientesModificar.direccion;

            _restauranteContexto.Entry(clientesActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(clientesActual);
        }

        [HttpGet]
        [Route("Find/{direccion}")]
        public IActionResult FindyBynombreDireccion(string Clientes)
        {
            Clientes? direccion = (from e in _restauranteContexto.Clientes
                             where e.direccion == Clientes
                             select e).FirstOrDefault();
            if (direccion == null)
            {
                return NotFound();
            }
            return Ok(direccion);
        }
    }
}
