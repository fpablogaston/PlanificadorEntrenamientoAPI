using System.ComponentModel.DataAnnotations;

namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class SerieCreateDTO
    {
        [Range(1, int.MaxValue)] 
        public int NumeroSerie { get; set; }

        [Range(1, int.MaxValue)]
        public int Repeticiones { get; set; }

        [Range(0.0, double.MaxValue)]
        public float Kg { get; set; }

        [Range(0, 10)]
        public int Rir { get; set; }

        [Range(1, int.MaxValue)]
        public int EjercicioId { get; set; }
    }
}
