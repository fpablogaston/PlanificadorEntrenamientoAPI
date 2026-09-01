using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Controller]
    [Authorize]
    public class EjerciciosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EjerciciosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Ejercicio> Get()
        {
            return _context.Ejercicios;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var obtenerId = _context.Ejercicios.FirstOrDefault(x => x.Id == id);
            
            if(obtenerId == null)
            {
                return NotFound();
            }

            return Ok(obtenerId);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ejercicio ejercicio)
        {
            _context.Ejercicios.Add(ejercicio);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ejercicio ejercicio)
        {
            var actualizarEjercicio = _context.Ejercicios.FirstOrDefault(x =>x.Id == id);

            if(actualizarEjercicio == null)
            {
                return NotFound();
            }

            actualizarEjercicio.Nombre = ejercicio.Nombre;
            actualizarEjercicio.Descripcion = ejercicio.Descripcion;
            actualizarEjercicio.DiaRutinaId = ejercicio.DiaRutinaId;
            actualizarEjercicio.UrlVideo = ejercicio.UrlVideo;
            actualizarEjercicio.UrlImagen = ejercicio.UrlImagen;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var EliminarEjercicio = _context.Ejercicios.FirstOrDefault(x =>x.Id == id);

            if (EliminarEjercicio == null)
            {
                return NotFound();
            }

            _context.Ejercicios.Remove(EliminarEjercicio);
            _context.SaveChanges();
            return Ok();
        }

    }
}
