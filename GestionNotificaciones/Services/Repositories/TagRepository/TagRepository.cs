using GestionNotificaciones.Data;
using GestionNotificaciones.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionNotificaciones.Services.Repositories.TagRepository
{
    public class TagRepository : ITagRepository
    {
        private readonly GestionDbContext gestionDbContext;

        public TagRepository(GestionDbContext gestionDbContext)
        {
            this.gestionDbContext = gestionDbContext;
        }

        public async Task<IEnumerable<Tag>> ObtenerTodos()
        {
            return await gestionDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> Obtener(Guid id)
        {
            return await gestionDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Tag> Agregar(Tag tag)
        {
            await gestionDbContext.Tags.AddAsync(tag);
            await gestionDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> Editar(Tag tag)
        {
            var existeTag = await gestionDbContext.Tags.FindAsync(tag.Id);

            if (existeTag != null)
            {
                existeTag.Nombre = tag.Nombre;
                existeTag.Descripcion = tag.Descripcion;

                await gestionDbContext.SaveChangesAsync();

                return existeTag;
            }

            return null;
        }

        public async Task<Tag?> Eliminar(Guid id)
        {
            var existeTag = await gestionDbContext.Tags.FindAsync(id);

            if (existeTag != null)
            {
                gestionDbContext.Tags.Remove(existeTag);
                await gestionDbContext.SaveChangesAsync();

                return existeTag;
            }

            return null;
        }
    }
}
