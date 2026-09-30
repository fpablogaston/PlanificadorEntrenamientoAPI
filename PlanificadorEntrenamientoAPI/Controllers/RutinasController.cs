using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.DTOs;
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
        public IEnumerable<RutinaResponseDTO> Get()
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto); 

            return _context.Rutinas
                 .Include(r => r.DiasRutina)
                 .Include(r => r.Alumno)
                 .Include(r => r.Entrenador)
                 .Where(r => r.EntrenadorId == entrenadorId)
                 .Select(r => new RutinaResponseDTO
                 {
                     Id = r.Id,
                     Nombre = r.Nombre,
                     AlumnoId = r.AlumnoId,
                     EntrenadorId = r.EntrenadorId,
                     Alumno = new UsuarioResumenDTO { Nombre = r.Alumno.Nombre },   
                     Entrenador = new UsuarioResumenDTO { Nombre = r.Entrenador.Nombre},
                     DiasRutina = r.DiasRutina.Select(d => new DiaRutinaResponseDTO
                     {
                         Id = d.Id,
                         NombreDia = d.NombreDia,
                         RutinaId = d.RutinaId
                     }).ToList()
                 })
                 .ToList();
        }

        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var rutina = _context.Rutinas
                .Include(r => r.DiasRutina)
                .Include(r => r.Alumno)
                .Include(r => r.Entrenador)
                .Where(r => r.Id == id && r.EntrenadorId == entrenadorId)

                .Select(r => new RutinaResponseDTO
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    AlumnoId = r.AlumnoId,
                    EntrenadorId = r.EntrenadorId,
                    Alumno = new UsuarioResumenDTO { Nombre = r.Alumno.Nombre },
                    Entrenador = new UsuarioResumenDTO { Nombre = r.Entrenador.Nombre },
                    DiasRutina = r.DiasRutina.Select(d => new DiaRutinaResponseDTO
                    {
                        Id = d.Id,
                        NombreDia = d.NombreDia,
                        RutinaId = d.RutinaId
                    }).ToList()
                })
                .FirstOrDefault();

            if (rutina == null)
            {
                return NotFound();
            }

            return Ok(rutina);
        }


        [HttpPost]
        public IActionResult Post([FromBody]RutinaCreateDTO rutinaDTO)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var nuevaRutina = new Rutina
            {
                Nombre = rutinaDTO.Nombre,
                AlumnoId = rutinaDTO.AlumnoId,
                EntrenadorId = entrenadorId
            };

            _context.Rutinas.Add(nuevaRutina);
            _context.SaveChanges();

            var respuesta = new RutinaResponseDTO
            {
                Id = nuevaRutina.Id,
                Nombre = nuevaRutina.Nombre,
                AlumnoId = nuevaRutina.AlumnoId,
                EntrenadorId = nuevaRutina.EntrenadorId
            };
            return Ok(respuesta);
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
            return NoContent();
        }
        

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] RutinaCreateDTO rutinaDTO, int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var alumnoValido = _context.Usuarios.Any(d => d.Id == rutinaDTO.AlumnoId && d.EntrenadorId == entrenadorId);
            if (!alumnoValido)
            {
                return NotFound();
            }

            var ModificarRutina = _context.Rutinas.FirstOrDefault(x =>x.Id == id && x.EntrenadorId == entrenadorId);

            if(ModificarRutina == null)
            {
                return NotFound();
            }

            ModificarRutina.Nombre = rutinaDTO.Nombre;
            ModificarRutina.AlumnoId = rutinaDTO.AlumnoId;

            _context.SaveChanges();

            var respuesta = new RutinaResponseDTO
            {
                Id = ModificarRutina.Id,
                Nombre = ModificarRutina.Nombre,
                AlumnoId = ModificarRutina.AlumnoId,
                EntrenadorId = ModificarRutina.EntrenadorId
            };
            return Ok(respuesta);
        }

    }
}
