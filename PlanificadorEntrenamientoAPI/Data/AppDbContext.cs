using Microsoft.EntityFrameworkCore;
using PlanificadorEntrenamientoAPI.Models;

namespace PlanificadorEntrenamientoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<DiaRutina> DiasRutina { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<Serie> Series { get; set; }

    }
}
