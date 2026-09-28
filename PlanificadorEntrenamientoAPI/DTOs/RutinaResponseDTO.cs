using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class RutinaResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int AlumnoId { get; set; }
        public UsuarioResumenDTO? Alumno { get; set; }
        public int EntrenadorId { get; set; }
        public UsuarioResumenDTO? Entrenador { get; set; }

        public List<DiaRutinaResponseDTO> DiasRutina { get; set; } = new();
    }
}

