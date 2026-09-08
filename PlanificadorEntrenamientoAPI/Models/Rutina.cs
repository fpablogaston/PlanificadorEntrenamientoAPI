namespace PlanificadorEntrenamientoAPI.Models
{
    public class Rutina
    {
        public int Id { get; set; } 
        public string? Nombre { get; set; }
        public int AlumnoId    { get; set; }
        public Usuario? Alumno {  get; set; }
        public int EntrenadorId { get; set; }
        public Usuario? Entrenador { get; set; }

        public List<DiaRutina> DiasRutina {  get; set; } = new();
    }
}
