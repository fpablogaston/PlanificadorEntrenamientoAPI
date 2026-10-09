using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.DTOs;
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
        public IEnumerable<EjercicioResponseDTO> Get()
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var rolTexto = User.FindFirst("rol")?.Value;

            List<EjercicioResponseDTO> ejercicioRutina;

            if(rolTexto == "Entrenador")
            {
              ejercicioRutina =  _context.Ejercicios
                    .Include(e => e.DiaRutina)
                    .ThenInclude(d => d.Rutina)
                    .Where(e => e.DiaRutina.Rutina.EntrenadorId == entrenadorId)
                    .Select(e => new EjercicioResponseDTO
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Descripcion = e.Descripcion,
                        UrlImagen = e.UrlImagen,
                        UrlVideo = e.UrlVideo,
                        DiaRutinaId = e.DiaRutinaId,
                    })
                    .ToList();
            } else
            {
                ejercicioRutina = _context.Ejercicios
                    .Include(e => e.DiaRutina)
                    .ThenInclude(d => d.Rutina)
                    .Where(e => e.DiaRutina.Rutina.AlumnoId == entrenadorId)
                    .Select(e => new EjercicioResponseDTO
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Descripcion = e.Descripcion,
                        UrlImagen = e.UrlImagen,
                        UrlVideo = e.UrlVideo,
                        DiaRutinaId = e.DiaRutinaId,
                    })
                    .ToList();
            }

            return ejercicioRutina;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ejercicio = _context.Ejercicios
                .Include(e => e.DiaRutina)
                .ThenInclude(d => d.Rutina)
                .Where(e => e.Id == id && e.DiaRutina.Rutina.EntrenadorId == entrenadorId)
                .Select(e => new EjercicioResponseDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion,
                    UrlImagen = e.UrlImagen,
                    UrlVideo = e.UrlVideo,
                    DiaRutinaId = e.DiaRutinaId,
                })
                .FirstOrDefault();
            
            if(ejercicio == null)
            {
                return NotFound();
            }

            return Ok(ejercicio);
        }


        [HttpPost]
        public IActionResult Post([FromBody] EjercicioCreateDTO ejercicioDTO)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var diaValido = _context.DiasRutina.Any(d => d.Id == ejercicioDTO.DiaRutinaId && d.Rutina.EntrenadorId == entrenadorId);
            if (!diaValido)
            {
                return NotFound();
            }

            var nuevoEjercicio = new Ejercicio
            {
                Nombre = ejercicioDTO?.Nombre,
                Descripcion = ejercicioDTO?.Descripcion,
                UrlImagen = ejercicioDTO?.UrlImagen,
                UrlVideo = ejercicioDTO?.UrlVideo,
                DiaRutinaId = ejercicioDTO.DiaRutinaId
            };

            _context.Ejercicios.Add(nuevoEjercicio);
            _context.SaveChanges();

            var respuesta = new EjercicioResponseDTO
            {
                Id = nuevoEjercicio.Id,
                Nombre = nuevoEjercicio.Nombre,
                Descripcion = nuevoEjercicio.Descripcion,
                UrlImagen = nuevoEjercicio.UrlImagen,
                UrlVideo = nuevoEjercicio.UrlVideo,
                DiaRutinaId = nuevoEjercicio.DiaRutinaId,
            };

            return Ok(respuesta);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EjercicioCreateDTO ejercicioDTO)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var diaValido = _context.DiasRutina.Any(d => d.Id == ejercicioDTO.DiaRutinaId && d.Rutina.EntrenadorId == entrenadorId);
            if (!diaValido)
            {
                return NotFound();
            }

            var actualizarEjercicio = _context.Ejercicios
                .Include(e => e.DiaRutina)
                .ThenInclude(d => d.Rutina)
                .FirstOrDefault(e => e.Id == id && e.DiaRutina.Rutina.EntrenadorId == entrenadorId);

            if(actualizarEjercicio == null)
            {
                return NotFound();
            }

            actualizarEjercicio.Nombre = ejercicioDTO.Nombre;
            actualizarEjercicio.Descripcion = ejercicioDTO.Descripcion;
            actualizarEjercicio.DiaRutinaId = ejercicioDTO.DiaRutinaId;
            actualizarEjercicio.UrlVideo = ejercicioDTO.UrlVideo;
            actualizarEjercicio.UrlImagen = ejercicioDTO.UrlImagen;

            _context.SaveChanges();

            var respuesta = new EjercicioResponseDTO
            {
                Id = actualizarEjercicio.Id,
                Nombre = actualizarEjercicio.Nombre,
                Descripcion = actualizarEjercicio.Descripcion,
                DiaRutinaId = actualizarEjercicio.DiaRutinaId,
                UrlVideo = actualizarEjercicio.UrlVideo,
                UrlImagen = actualizarEjercicio.UrlImagen
            };

            return Ok(respuesta);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var EliminarEjercicio = _context.Ejercicios
                .Include(e => e.DiaRutina)
                .ThenInclude(d => d.Rutina)
                .Where(e => e.Id == id && e.DiaRutina.Rutina.EntrenadorId == entrenadorId)
                .FirstOrDefault();

            if (EliminarEjercicio == null)
            {
                return NotFound();
            }

            _context.Ejercicios.Remove(EliminarEjercicio);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
