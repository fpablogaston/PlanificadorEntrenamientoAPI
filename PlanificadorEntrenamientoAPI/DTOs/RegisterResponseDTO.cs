using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class RegisterResponseDTO
    {
        public string? Nombre { get; set; }
        public RolUsuario Rol { get; set; }
        public string? Token { get; set; }

    }
}
