namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class SerieResponseDTO
    {
        public int Id { get; set; }
        public int NumeroSerie { get; set; }
        public int Repeticiones { get; set; }
        public float Kg { get; set; }
        public int Rir { get; set; }
        public int EjercicioId { get; set; }
    }
}
