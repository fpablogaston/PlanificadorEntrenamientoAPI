using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.Models;


namespace PlanificadorEntrenamientoAPI.Controllers
{
    [ApiController]
    [Controller]
    [Route("api/[controller]")]
    [Authorize]

    public class RutinasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RutinasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Rutina> Get()
        {

            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto); 

            return _context.Rutinas
                 .Include(r => r.DiasRutina)
                 .ThenInclude(d => d.Ejercicios)
                 .ThenInclude(e => e.Series)
                 .Where(r => r.EntrenadorId == entrenadorId)
                 .ToList();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ObtenerId = _context.Rutinas.FirstOrDefault(x => x.Id == id && x.EntrenadorId == entrenadorId);
            if (ObtenerId == null)
            {
                return NotFound();
            }

            return Ok(ObtenerId);
        }


        [HttpPost]
        public IActionResult Post([FromBody]Rutina rutina)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            rutina.EntrenadorId = entrenadorId;
            _context.Rutinas.Add(rutina);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var EliminarRutina = _context.Rutinas.FirstOrDefault(x => x.Id == id && x.EntrenadorId == entrenadorId);
            if (EliminarRutina == null)
            {
                return NotFound();
            }
            _context.Rutinas.Remove(EliminarRutina);
            _context.SaveChanges();
            return Ok();
        }
        

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Rutina rutina, int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ModificarRutina = _context.Rutinas.FirstOrDefault(x =>x.Id == id && x.EntrenadorId == entrenadorId);

            if(ModificarRutina == null)
            {
                return NotFound();
            }

            ModificarRutina.Nombre = rutina.Nombre;
            ModificarRutina.AlumnoId = rutina.AlumnoId;
            ModificarRutina.Alumno = rutina.Alumno;

            _context.SaveChanges();
            return Ok();
        }
    }
}
