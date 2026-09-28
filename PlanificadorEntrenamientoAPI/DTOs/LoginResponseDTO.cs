using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class LoginResponseDTO
    {
        public string? Nombre { get; set; }
        public RolUsuario Rol { get; set; }
        public string? Token { get; set; }
    }
}
