using System.ComponentModel.DataAnnotations;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class DiaRutinaCreateDTO
    {
        [Required]
        public string? NombreDia { get; set; }
        [Range(1, int.MaxValue)]
        public int RutinaId { get; set; }

    }
}
