namespace PlanificadorEntrenamientoAPI.Models
{
    public class Rutina
    {
        public int Id { get; set; } 
        public string? Nombre { get; set; }
        public int UsuarioId    { get; set; }
        public Usuario? Usuario {  get; set; }

        public List<DiaRutina>? DiasRutina {  get; set; }
    }
}
