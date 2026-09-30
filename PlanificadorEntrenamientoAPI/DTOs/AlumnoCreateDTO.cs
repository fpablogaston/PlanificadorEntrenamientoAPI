using System.ComponentModel.DataAnnotations;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class AlumnoCreateDTO
    {
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
