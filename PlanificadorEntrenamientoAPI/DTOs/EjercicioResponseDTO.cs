namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class EjercicioResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public string? UrlVideo { get; set; }
        public int DiaRutinaId { get; set; }
    }
}
