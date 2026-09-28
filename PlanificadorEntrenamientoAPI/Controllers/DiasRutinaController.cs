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
    public class DiasRutinaController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public DiasRutinaController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<DiaRutinaResponseDTO> Get()
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            return _context.DiasRutina
                .Include(d => d.Rutina)
                .Where(d => d.Rutina.EntrenadorId == entrenadorId)
                .Select(d => new DiaRutinaResponseDTO
                {
                    Id = d.Id,
                    NombreDia = d.NombreDia,
                    RutinaId = d.RutinaId,
                })
                .ToList();
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var dia = _context.DiasRutina
                .Include(d => d.Rutina)
                .Where(d => d.Id == id && d.Rutina.EntrenadorId == entrenadorId)
                .Select(d => new DiaRutinaResponseDTO
                {
                    Id = d.Id,
                    NombreDia = d.NombreDia,
                    RutinaId = d.RutinaId,
                })
                .FirstOrDefault();

            if (dia == null)
            {
                return NotFound();
            }
            
            return Ok(dia);
        } 


        [HttpPost]
        public IActionResult Post([FromBody] DiaRutinaCreateDTO diaRutinaDTO)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var rutinaValida = _context.Rutinas.Any(r => r.Id == diaRutinaDTO.RutinaId && r.EntrenadorId == entrenadorId);
            if (!rutinaValida)
            {
                return NotFound();
            }

            var nuevoDia = new DiaRutina
            {
                NombreDia = diaRutinaDTO.NombreDia,
                RutinaId = diaRutinaDTO.RutinaId
            };

            _context.DiasRutina.Add(nuevoDia);
            _context.SaveChanges();

            var respuesta = new DiaRutinaResponseDTO
            {
                Id = nuevoDia.Id,
                NombreDia = nuevoDia.NombreDia,
                RutinaId = nuevoDia.RutinaId,
            };

            return Ok(respuesta);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var eliminarDia = _context.DiasRutina
                .Include( d => d.Rutina)
                .FirstOrDefault(d => d.Id == id && d.Rutina.EntrenadorId == entrenadorId);

            if (eliminarDia == null)
            {
                return NotFound();
            }

            _context.DiasRutina.Remove(eliminarDia);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromBody] DiaRutinaCreateDTO diaRutinaDTO, int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var actualizarDia = _context.DiasRutina
                .Include(d => d.Rutina)
                .FirstOrDefault(d =>d.Id == id && d.Rutina.EntrenadorId == entrenadorId);


            if (actualizarDia == null)
            {
                return NotFound();
            }

            actualizarDia.NombreDia = diaRutinaDTO.NombreDia;
            actualizarDia.RutinaId = diaRutinaDTO.RutinaId;

            _context.SaveChanges();

            var respuesta = new DiaRutinaResponseDTO
            {
                Id = actualizarDia.Id,
                NombreDia = actualizarDia.NombreDia,
                RutinaId = actualizarDia.RutinaId,
            };

            return Ok(respuesta);
        }

    }
}
