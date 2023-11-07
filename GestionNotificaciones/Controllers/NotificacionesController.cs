using GestionNotificaciones.Services.Repositories.NotificacionRepository;
using Microsoft.AspNetCore.Mvc;

namespace GestionNotificaciones.Controllers
{
    public class NotificacionesController : Controller
    {
        private readonly INotificacionRepository notificacionRepository;

        public NotificacionesController(INotificacionRepository notificacionRepository)
        {
            this.notificacionRepository = notificacionRepository;
        }



        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var notificacion = await notificacionRepository.ObtenerPorUrlHandle(urlHandle);

            return View(notificacion);
        }
    }
}
