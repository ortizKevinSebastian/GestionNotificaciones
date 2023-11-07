using GestionNotificaciones.Models.Entities;

namespace GestionNotificaciones.Services.Repositories.TagRepository
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> ObtenerTodos();

        Task<Tag?> Obtener(Guid id);

        Task<Tag> Agregar(Tag tag);

        Task<Tag?> Editar(Tag tag);

        Task<Tag?> Eliminar(Guid id);
    }
}
