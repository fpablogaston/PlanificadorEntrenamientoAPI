using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.DTOs;
using PlanificadorEntrenamientoAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PlanificadorEntrenamientoAPI.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(AppDbContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]UsuarioCreateDTO usuario)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Password = usuario.Password,
                Rol = usuario.Rol,
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
            return Ok(nuevoUsuario);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO usuario)
        {
            var encontrarUsuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);
            if(encontrarUsuario == null)
            {
                return Unauthorized();
            }

            if(encontrarUsuario.Password != usuario.Password)
            {
                return Unauthorized();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: null,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }
        
    }
}
