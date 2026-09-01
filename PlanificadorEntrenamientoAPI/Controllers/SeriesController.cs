using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.Controllers
{
    [ApiController]
    [Controller]
    [Route("api/[controller]")]
    [Authorize]
    public class SeriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SeriesController(AppDbContext context) 
        { 
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Serie> Get()
        {
            return _context.Series;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ObtenerId = _context.Series.FirstOrDefault(x  => x.Id == id);
            if(ObtenerId == null)
            {
                return NotFound();
            }

            return Ok(ObtenerId);
        }

        [HttpPost]
        public IActionResult Post ([FromBody] Serie serie)
        {
            _context.Series.Add(serie);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            var eliminarSerie = _context.Series.FirstOrDefault(x =>x.Id == id);
            if(eliminarSerie == null)
            {
                return NotFound();
            }

            _context.Series.Remove(eliminarSerie);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Serie serie, int id)
        {
            var actualizarSerie = _context.Series.FirstOrDefault(x =>x.Id == id);
            if(actualizarSerie == null)
            {
                return NotFound();
            }

            actualizarSerie.Rir = serie.Rir;
            actualizarSerie.EjercicioId = serie.EjercicioId;
            actualizarSerie.Repeticiones = serie.Repeticiones;
            actualizarSerie.NumeroSerie = serie.NumeroSerie;
            actualizarSerie.Kg = serie.Kg;


            _context.SaveChanges();
            return Ok();
        }
    }
}
