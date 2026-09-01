namespace PlanificadorEntrenamientoAPI.Models
{
    public class Ejercicio
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion {  get; set; }
        public string? UrlImagen { get; set; }
        public string? UrlVideo  { get; set; }
        public int DiaRutinaId  { get; set; }
        public DiaRutina? DiaRutina { get; set; }

        public List<Serie>? Series { get; set; }

    }
}
