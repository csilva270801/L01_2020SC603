using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020SC603.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020SC603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public PlatosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Platos> listadoPlatos = (from e in _restauranteContexto.Platos
                                            select e).ToList();
            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatos);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPlatos([FromBody] Platos platos)
        {
            try
            {
                _restauranteContexto.Platos.Add(platos);
                _restauranteContexto.SaveChanges();
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarPlatos(int platoId)
        {
            Platos? platos = (from e in _restauranteContexto.Platos
                                where e.platoId == platoId
                                select e).FirstOrDefault();
            if (platos == null)
                return NotFound();

            _restauranteContexto.Platos.Attach(platos);
            _restauranteContexto.Platos.Remove(platos);
            _restauranteContexto.SaveChanges();

            return Ok(platos);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Platos platosModificar)
        {
            Platos? platosActual = (from e in _restauranteContexto.Platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();
            if (platosActual == null)
            {
                return NotFound();
            }

            platosActual.nombrePlato = platosModificar.nombrePlato;
            platosActual.precio = platosModificar.precio;

            _restauranteContexto.Entry(platosActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();

            return Ok(platosActual);
        }

        [HttpGet]
        [Route("Find/{nombrePlato}")]
        public IActionResult FindyBynombrePlato(string nombrePlato)
        {
            Platos? plato = (from e in _restauranteContexto.Platos
                               where e.nombrePlato == nombrePlato
                             select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }
    }
}
