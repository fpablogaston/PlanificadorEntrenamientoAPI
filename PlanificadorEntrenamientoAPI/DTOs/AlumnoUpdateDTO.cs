using System.ComponentModel.DataAnnotations;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class AlumnoUpdateDTO
    {
        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Apellido { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
