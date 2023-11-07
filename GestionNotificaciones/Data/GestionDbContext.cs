using Azure;
using GestionNotificaciones.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionNotificaciones.Data
{
    public class GestionDbContext : DbContext
    {
        public GestionDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Notificacion> Notificaciones { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
