using GestionNotificaciones.Models.Entities;
using GestionNotificaciones.Models.ViewModels;
using GestionNotificaciones.Services.Repositories.NotificacionRepository;
using GestionNotificaciones.Services.Repositories.TagRepository;
using GestionNotificaciones.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionNotificaciones.Controllers
{
    public class GestionNotificacionesController : Controller
    {
        private readonly INotificacionRepository notificacionRepository;
        private readonly ITagRepository tagRepository;

        public GestionNotificacionesController(INotificacionRepository notificacionRepository, ITagRepository tagRepository)
        {
            this.notificacionRepository = notificacionRepository;
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            var tags = await tagRepository.ObtenerTodos();

            var model = new AgregarNotifRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(AgregarNotifRequest agregarNotifRequest)
        {
            if (Validaciones.CampoVacioAgregar(agregarNotifRequest))
            {
                //validacion, campos requeridos.
                return RedirectToAction("Agregar");
            }


            //no puedo pasar el parámetro directamente, mapeo primero la ViewModel a la Entity Model Notificacion
            var notificacion = new Notificacion
            {
                Encabezado = agregarNotifRequest.Encabezado,
                TituloPagina = agregarNotifRequest.TituloPagina,
                Texto = agregarNotifRequest.Texto,
                Contenido = agregarNotifRequest.Contenido,
                UrlHandle = agregarNotifRequest.UrlHandle,
                Fecha = agregarNotifRequest.Fecha,
                Visible = agregarNotifRequest.Visible,
            };

            //tambien mapeo los tags desde agregarNotifRequest.Selecionado
            var listaTags = new List<Tag>();

            foreach (var tagsSelecionados in agregarNotifRequest.Selecionado)
            {
                var idTagsSelecionados = Guid.Parse(tagsSelecionados);
                var existeTag = await tagRepository.Obtener(idTagsSelecionados);

                if (existeTag != null)
                {
                    listaTags.Add(existeTag);
                }
            }
            //por ultimo tambien mapeo los tags a la entity model Notificacion
            notificacion.Tags = listaTags;
            await notificacionRepository.Agregar(notificacion);


            return RedirectToAction("Lista");
        }


        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var notificaciones = await notificacionRepository.ObtenerTodos();

            return View(notificaciones);
        }


        [HttpGet]
        [ActionName("Editar")]
        public async Task<IActionResult> Editar(Guid id)
        {
            //recupero la info del Repository
            var notificaciones = await notificacionRepository.Obtener(id);
            var tags = await tagRepository.ObtenerTodos();


            if (notificaciones != null)
            {
                //mapeo la entity model en la ViewModel
                var model = new EditarNotifRequest
                {
                    Id = notificaciones.Id,
                    Encabezado = notificaciones.Encabezado,
                    TituloPagina = notificaciones.TituloPagina,
                    Texto = notificaciones.Texto,
                    Contenido = notificaciones.Contenido,
                    UrlHandle = notificaciones.UrlHandle,
                    Fecha = notificaciones.Fecha,
                    Visible = notificaciones.Visible,
                    Tags = tags.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() }),
                    Selecionado = notificaciones.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarNotifRequest editarNotifRequest)
        {
            if (Validaciones.CampoVacioEditar(editarNotifRequest))
            {
                //validacion de campos requeridos
                return View();
            }
            
            //mapeo la ViewModel a la entity model Notificacion
            var notificacion = new Notificacion
            {
                Id = editarNotifRequest.Id,
                Encabezado = editarNotifRequest.Encabezado,
                TituloPagina = editarNotifRequest.TituloPagina,
                Texto = editarNotifRequest.Texto,
                Contenido = editarNotifRequest.Contenido,
                UrlHandle = editarNotifRequest.UrlHandle,
                Fecha = editarNotifRequest.Fecha,
                Visible = editarNotifRequest.Visible,
            };

            //Mapeo los tags a la entity model
            var listaTags = new List<Tag>();

            foreach (var tagSeleccionado in editarNotifRequest.Selecionado)
            {
                if (Guid.TryParse(tagSeleccionado, out var tag))
                {
                    var tagEncontrado = await tagRepository.Obtener(tag);

                    if (tagEncontrado != null)
                    {
                        listaTags.Add(tagEncontrado);
                    }
                }
            }

            notificacion.Tags = listaTags;

            //paso todo al Repository para que lo guarde en la Db
            var notificacionEditada = await notificacionRepository.Editar(notificacion);

            if (notificacionEditada != null)
            {
                //"Realizado con exito"
                return RedirectToAction("Lista");
            }

            //"Error"
            return RedirectToAction("Editar");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(EditarNotifRequest editarNotifRequest)
        {
            var notificacionEliminada = await notificacionRepository.Eliminar(editarNotifRequest.Id);

            if (notificacionEliminada != null)
            {
                //"realizado con exito"
                return RedirectToAction("Lista");
            }

            //"error"
            return RedirectToAction("Editar", new { id = editarNotifRequest.Id });
        }
    }
}
