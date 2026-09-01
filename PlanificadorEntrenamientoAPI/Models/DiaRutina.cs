namespace PlanificadorEntrenamientoAPI.Models
{
    public class DiaRutina
    {
        public int Id { get; set; }
        public string? NombreDia { get; set; }
        public int RutinaId { get; set; }
        public Rutina? Rutina { get; set; }

        public List<Ejercicio>? Ejercicios { get; set; }

    }
}
