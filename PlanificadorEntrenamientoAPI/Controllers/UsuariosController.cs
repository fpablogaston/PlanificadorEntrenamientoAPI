using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.DTOs;
using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.Controllers
{
    [Authorize]
    [Controller]
    [ApiController]
    [Route("api/[controller]")]

    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<AlumnoResponseDTO> Get()
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            return _context.Usuarios
                .Where(a => a.EntrenadorId == entrenadorId)
                .Select(a => new AlumnoResponseDTO
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Apellido = a.Apellido,
                    Email = a.Email,
                })
                .ToList();

        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ObtenerId = _context.Usuarios
                .Where(a => a.Id == id && a.EntrenadorId == entrenadorId)
                .Select(a => new AlumnoResponseDTO
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Apellido = a.Apellido,
                    Email = a.Email,
                })
                .FirstOrDefault();

            if (ObtenerId == null)
            {
                return NotFound();
            }
            return Ok(ObtenerId); 
        }


        [HttpPost]
        public IActionResult Post([FromBody] AlumnoCreateDTO alumnoDTO)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var nuevoUsuario = new Usuario
            {
                Nombre = alumnoDTO.Nombre,
                Apellido = alumnoDTO.Apellido,
                Email = alumnoDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(alumnoDTO.Password),
                Rol = RolUsuario.Alumno,
                EntrenadorId = entrenadorId,
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            var respuesta = new AlumnoResponseDTO
            {
                Id = nuevoUsuario.Id,
                Nombre = nuevoUsuario.Nombre,
                Apellido = alumnoDTO.Apellido,
                Email = alumnoDTO.Email,
            };

            return Ok(respuesta);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var EliminarUsuario = _context.Usuarios
                .FirstOrDefault(x => x.Id == id && x.EntrenadorId == entrenadorId);

            if(EliminarUsuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(EliminarUsuario);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromBody] AlumnoUpdateDTO alumnoDTO, int id)
        {
            var entrenadorIdTexto = User.FindFirst("userId")?.Value;
            var entrenadorId = int.Parse(entrenadorIdTexto);

            var ActualizarUsuario = _context.Usuarios
                .FirstOrDefault(x =>x.Id == id && x.EntrenadorId == entrenadorId);

            if(ActualizarUsuario == null)
            {
                return NotFound();
            }

            ActualizarUsuario.Nombre = alumnoDTO.Nombre;
            ActualizarUsuario.Apellido = alumnoDTO.Apellido;
            ActualizarUsuario.Rol = RolUsuario.Alumno;
            ActualizarUsuario.Email = alumnoDTO.Email;

            _context.SaveChanges();

            var respuesta = new AlumnoResponseDTO
            {
                Id = ActualizarUsuario.Id,
                Nombre = ActualizarUsuario?.Nombre,
                Apellido = ActualizarUsuario?.Apellido,
                Email = ActualizarUsuario?.Email,
            };

            return Ok(respuesta);
        }
        
    }
}
