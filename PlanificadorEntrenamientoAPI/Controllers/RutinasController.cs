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
            return _context.Rutinas;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ObtenerId = _context.Rutinas.FirstOrDefault(x => x.Id == id);
            if (ObtenerId == null)
            {
                return NotFound();
            }

            return Ok(ObtenerId);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Rutina rutina)
        {
            _context.Rutinas.Add(rutina);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var EliminarRutina = _context.Rutinas.FirstOrDefault(x => x.Id == id);
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
            var ModificarRutina = _context.Rutinas.FirstOrDefault(x =>x.Id == id);

            if(ModificarRutina == null)
            {
                return NotFound();
            }

            ModificarRutina.Nombre = rutina.Nombre;
            ModificarRutina.UsuarioId = rutina.UsuarioId;
            ModificarRutina.Usuario = rutina.Usuario;

            _context.SaveChanges();
            return Ok();
        }

    }
}
