using GestionNotificaciones.Models.Entities;

namespace GestionNotificaciones.Services.Repositories.NotificacionRepository
{
    public interface INotificacionRepository
    {
        Task<IEnumerable<Notificacion>> ObtenerTodos();

        Task<Notificacion?> Obtener(Guid id);

        Task<Notificacion?> ObtenerPorUrlHandle(string urlHandle);

        Task<Notificacion> Agregar(Notificacion notificacion);

        Task<Notificacion?> Editar(Notificacion notificacion);

        Task<Notificacion?> Eliminar(Guid id);
    }
}
