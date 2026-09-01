namespace PlanificadorEntrenamientoAPI.DTOs
{
    public class UsuarioCreateDTO
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Rol {  get; set; }

    }
}
