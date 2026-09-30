using System.ComponentModel.DataAnnotations;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class EjercicioCreateDTO
    {
        [Required]
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Url]
        public string? UrlImagen { get; set; }
        [Url]
        public string? UrlVideo { get; set; }
        [Range(1, int.MaxValue)]
        public int DiaRutinaId { get; set; }
    }
}
