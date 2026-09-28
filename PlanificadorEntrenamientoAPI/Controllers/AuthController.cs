using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanificadorEntrenamientoAPI.Data;
using PlanificadorEntrenamientoAPI.DTOs;
using PlanificadorEntrenamientoAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

           var rolUsuario =  Enum.TryParse<RolUsuario>(usuario.Rol, out var rolConvertido);
           if (rolUsuario == false)
           {
              return BadRequest("El usuario es invalido");
           }
           
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Password = usuario.Password,
                Rol = rolConvertido,
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("userId", nuevoUsuario.Id.ToString()),
                new Claim("email", nuevoUsuario.Email ?? ""),
                new Claim("rol", rolConvertido.ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var respuesta = new RegisterResponseDTO
            {
                Nombre = nuevoUsuario.Nombre,
                Rol = nuevoUsuario.Rol,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(respuesta);
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

            var claims = new[]
            {
                new Claim("userId", encontrarUsuario.Id.ToString()),
                new Claim("email", encontrarUsuario.Email ?? ""),
                new Claim("rol", encontrarUsuario.Rol.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var respuesta = new LoginResponseDTO
            {
                Nombre = encontrarUsuario.Nombre,
                Rol = encontrarUsuario.Rol,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(respuesta);
        }
        
    }
}
