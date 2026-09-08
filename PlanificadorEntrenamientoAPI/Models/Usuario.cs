namespace PlanificadorEntrenamientoAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RolUsuario Rol {  get; set; }

        public int? EntrenadorId { get; set; }
        public Usuario? Entrenador { get; set; }

        public ICollection<Usuario>? Alumnos { get; set; }
    }
}
