using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.Controllers
{
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
        public IEnumerable<Usuario> Get()
        {
            return _context.Usuarios;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ObtenerId = _context.Usuarios.FirstOrDefault(x => x.Id == id);
            if (ObtenerId == null)
            {
                return NotFound();
            }
            return Ok(ObtenerId); 
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var EliminarUsuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);
            if(EliminarUsuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(EliminarUsuario);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Usuario usuario, int id)
        {
            var ActualizarUsuario = _context.Usuarios.FirstOrDefault(x =>x.Id == id);
            if(ActualizarUsuario == null)
            {
                return NotFound();
            }

            ActualizarUsuario.Nombre = usuario.Nombre;
            ActualizarUsuario.Apellido = usuario.Apellido;
            ActualizarUsuario.Rol = usuario.Rol;
            ActualizarUsuario.Email = usuario.Email;
            ActualizarUsuario.Password = usuario.Password;

            _context.SaveChanges();
            return Ok();
        }

    }
}
