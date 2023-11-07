using GestionNotificaciones.Data;
using GestionNotificaciones.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionNotificaciones.Services.Repositories.NotificacionRepository
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly GestionDbContext gestionDbContext;

        public NotificacionRepository(GestionDbContext gestionDbContext)
        {
            this.gestionDbContext = gestionDbContext;
        }

        public async Task<Notificacion> Agregar(Notificacion notificacion)
        {
            await gestionDbContext.AddAsync(notificacion);
            await gestionDbContext.SaveChangesAsync();

            return notificacion;
        }

        public async Task<IEnumerable<Notificacion>> ObtenerTodos()
        {
            return await gestionDbContext.Notificaciones.Include(x => x.Tags).ToListAsync();
        }

        public async Task<Notificacion?> Obtener(Guid id)
        {
            return await gestionDbContext.Notificaciones.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Notificacion?> Editar(Notificacion notificacion)
        {
            var existeNotificacion = await gestionDbContext.Notificaciones.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == notificacion.Id);

            if (existeNotificacion != null)
            {
                existeNotificacion.Id = notificacion.Id;
                existeNotificacion.Encabezado = notificacion.Encabezado;
                existeNotificacion.TituloPagina = notificacion.TituloPagina;
                existeNotificacion.Texto = notificacion.Texto;
                existeNotificacion.Contenido = notificacion.Contenido;
                existeNotificacion.UrlHandle = notificacion.UrlHandle;
                existeNotificacion.Fecha = notificacion.Fecha;
                existeNotificacion.Visible = notificacion.Visible;
                existeNotificacion.Tags = notificacion.Tags;

                await gestionDbContext.SaveChangesAsync();
                return existeNotificacion;
            }

            return null;
        }

        public async Task<Notificacion?> Eliminar(Guid id)
        {
            var existeNotificacion = await gestionDbContext.Notificaciones.FindAsync(id);

            if (existeNotificacion != null)
            {
                gestionDbContext.Notificaciones.Remove(existeNotificacion);
                await gestionDbContext.SaveChangesAsync();

                return existeNotificacion;
            }

            return null;
        }

        public async Task<Notificacion?> ObtenerPorUrlHandle(string urlHandle)
        {
            return await gestionDbContext.Notificaciones.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }
    }
}
