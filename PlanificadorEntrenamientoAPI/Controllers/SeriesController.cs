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
    public class SeriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SeriesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<SerieResponseDTO> Get()
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);
            var rolTexto = User.FindFirst("rol")?.Value;

            List<SerieResponseDTO> serieRutina;
            
            if(rolTexto == "Entrenador")
            {
                serieRutina = _context.Series
                    .Include(s => s.Ejercicio)
                    .ThenInclude(e => e.DiaRutina)
                    .ThenInclude(d => d.Rutina)
                    .Where(s => s.Ejercicio.DiaRutina.Rutina.EntrenadorId == entrenadorId)
                    .Select(s => new SerieResponseDTO
                    {
                        Id = s.Id,
                        NumeroSerie = s.NumeroSerie,
                        Repeticiones = s.Repeticiones,
                        Kg = s.Kg,
                        Rir = s.Rir,
                        EjercicioId = s.EjercicioId,
                    })
                    .ToList();
            } else
            {
                serieRutina = _context.Series
                    .Include(s => s.Ejercicio)
                    .ThenInclude(e => e.DiaRutina)
                    .ThenInclude(d => d.Rutina)
                    .Where(s => s.Ejercicio.DiaRutina.Rutina.AlumnoId == entrenadorId)
                    .Select(s => new SerieResponseDTO
                    {
                        Id = s.Id,
                        NumeroSerie = s.NumeroSerie,
                        Repeticiones = s.Repeticiones,
                        Kg = s.Kg,
                        Rir = s.Rir,
                        EjercicioId = s.EjercicioId,
                    })
                    .ToList();
            }

            return serieRutina;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ObtenerId = _context.Series
                .Include(s => s.Ejercicio)
                 .ThenInclude(e => e.DiaRutina)
                .ThenInclude(d => d.Rutina)
                .Where(s => s.Id == id && s.Ejercicio.DiaRutina.Rutina.EntrenadorId == entrenadorId)

                .Select(s => new SerieResponseDTO
                {
                    Id = s.Id,
                    NumeroSerie = s.NumeroSerie,
                    Repeticiones = s.Repeticiones,
                    Kg = s.Kg,
                    Rir = s.Rir,
                    EjercicioId = s.EjercicioId,
                })
                .FirstOrDefault();

            if (ObtenerId == null)
            {
                return NotFound();
            }

            return Ok(ObtenerId);
        }


        [HttpPost]
        public IActionResult Post([FromBody] SerieCreateDTO serieDTO)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ejercicioValido = _context.Ejercicios.Any(e => e.Id == serieDTO.EjercicioId && e.DiaRutina.Rutina.EntrenadorId == entrenadorId);
            if (!ejercicioValido)
            {
                return NotFound();
            }

            var nuevaSerie = new Serie
            {
                NumeroSerie = serieDTO.NumeroSerie,
                Repeticiones = serieDTO.Repeticiones,
                Kg = serieDTO.Kg,
                Rir = serieDTO.Rir,
                EjercicioId = serieDTO.EjercicioId,
            };

            _context.Series.Add(nuevaSerie);
            _context.SaveChanges();


            var respuesta = new SerieResponseDTO
            {
                Id = nuevaSerie.Id,
                NumeroSerie = serieDTO.NumeroSerie,
                Repeticiones = serieDTO.Repeticiones,
                Kg = serieDTO.Kg,
                Rir = serieDTO.Rir,
                EjercicioId = serieDTO.EjercicioId,
            };

            return Ok(respuesta);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var eliminarSerie = _context.Series
                .Include(s => s.Ejercicio)
                .ThenInclude(e => e.DiaRutina)
                .ThenInclude(d => d.Rutina)
                .Where(s => s.Id == id && s.Ejercicio.DiaRutina.Rutina.EntrenadorId == entrenadorId)
                 .FirstOrDefault();

            if(eliminarSerie == null)
            {
                return NotFound();
            }

            _context.Series.Remove(eliminarSerie);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromBody] SerieCreateDTO serieDTO, int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ejercicioValido = _context.Ejercicios.Any(e => e.Id == serieDTO.EjercicioId && e.DiaRutina.Rutina.EntrenadorId == entrenadorId);
            if (!ejercicioValido)
            {
                return NotFound();
            }

            var actualizarSerie = _context.Series
                .Include(s => s.Ejercicio)
                .ThenInclude(e => e.DiaRutina)
                .ThenInclude(d => d.Rutina)
                .FirstOrDefault(s => s.Id == id && s.Ejercicio.DiaRutina.Rutina.EntrenadorId == entrenadorId);

            if(actualizarSerie == null)
            {
                return NotFound();
            }

            actualizarSerie.NumeroSerie = serieDTO.NumeroSerie;
            actualizarSerie.Repeticiones = serieDTO.Repeticiones;
            actualizarSerie.Kg = serieDTO.Kg;
            actualizarSerie.Rir = serieDTO.Rir;
            actualizarSerie.EjercicioId = serieDTO.EjercicioId;
            
            _context.SaveChanges();
            
            var respuesta = new SerieResponseDTO
            {
                Id = actualizarSerie.Id,
                NumeroSerie = actualizarSerie.NumeroSerie,
                Repeticiones = actualizarSerie.Repeticiones,
                Kg = actualizarSerie.Kg,
                Rir = actualizarSerie.Rir,
                EjercicioId = actualizarSerie.EjercicioId,
            };

            return Ok(respuesta);
        }

    }
}
