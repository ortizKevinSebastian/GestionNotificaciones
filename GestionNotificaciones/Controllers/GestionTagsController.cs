using GestionNotificaciones.Models.Entities;
using GestionNotificaciones.Models.ViewModels;
using GestionNotificaciones.Services.Repositories.TagRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestionNotificaciones.Controllers
{
    public class GestionTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public GestionTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult AgregarTag()
        {
            return View();
        }

        [HttpPost]
        //[ActionName("AgregarTag")]
        public async Task<IActionResult> AgregarTag(AgregarTagRequest agregarTagRequest)
        {
            if (!ModelState.IsValid)
            {
                //Requerido
                return View();
            }

            //mapeo agregarTagRequest al tag entity. Tomo lo que llega desde agregarTagRequest y lo convierto en tipo Tag
            var tag = new Tag
            {
                Nombre = agregarTagRequest.Nombre,
                Descripcion = agregarTagRequest.Descripcion,
            };

            await tagRepository.Agregar(tag);

            return RedirectToAction("Lista");
        }


        [HttpGet]
        [ActionName("Lista")]
        public async Task<IActionResult> Lista()
        {
            //uso el repositorio que se comunica con DbContext para leer los tags almacenados.
            var tags = await tagRepository.ObtenerTodos();

            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> EditarTag(Guid id)
        {
            var tag = await tagRepository.Obtener(id);

            if (tag != null)
            {
                var editarTagRequest = new EditarTagRequest
                {
                    Id = tag.Id,
                    Nombre = tag.Nombre,
                    Descripcion = tag.Descripcion,
                };

                return View(editarTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditarTag(EditarTagRequest editarTagRequest)
        {
            if (!ModelState.IsValid)
            {
                //Anotación Requerido               
                return View();
                //return RedirectToAction("EditarTag", new { id = editarTagRequest.Id });
            }

            var tag = new Tag
            {
                Id = editarTagRequest.Id,
                Nombre = editarTagRequest.Nombre,
                Descripcion = editarTagRequest.Descripcion,
            };

            var editado = await tagRepository.Editar(tag);

            return RedirectToAction("Lista");
        }


        [HttpPost]
        public async Task<IActionResult> EliminarTag(EditarTagRequest editarTagRequest)
        {
            var eliminado = await tagRepository.Eliminar(editarTagRequest.Id);

            if (eliminado != null)
            {
                //"realizado con exito"
                return RedirectToAction("Lista");
            }

            //"error"
            return RedirectToAction("EditarTag", new { id = editarTagRequest.Id });
        }
    }
}
