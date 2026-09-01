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
    public class DiasRutinaController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public DiasRutinaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<DiaRutina> Get()
        {
            return _context.DiasRutina;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ObtenerId = _context.DiasRutina.FirstOrDefault(d => d.Id == id);
            if (ObtenerId == null)
            {
                return NotFound();
            }
            return Ok(ObtenerId);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DiaRutina diaRutina)
        {
            _context.DiasRutina.Add(diaRutina);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var eliminarDia = _context.DiasRutina.FirstOrDefault(x => x.Id == id);
            if (eliminarDia == null)
            {
                return NotFound();
            }
            _context.DiasRutina.Remove(eliminarDia);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] DiaRutina diaRutina, int id)
        {
            var actualizarDia = _context.DiasRutina.FirstOrDefault(x =>x.Id == id);
            if (actualizarDia == null)
            {
                return NotFound();
            }

            actualizarDia.NombreDia = diaRutina.NombreDia;
            actualizarDia.RutinaId = diaRutina.RutinaId;

            _context.SaveChanges();
            return Ok();
        }

    }
}
