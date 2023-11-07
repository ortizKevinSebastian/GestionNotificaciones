using GestionNotificaciones.Models;
using GestionNotificaciones.Services.Repositories.NotificacionRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestionNotificaciones.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotificacionRepository notificacionRepository;

        public HomeController(ILogger<HomeController> logger, INotificacionRepository notificacionRepository)
        {
            _logger = logger;
            this.notificacionRepository = notificacionRepository;
        }


        public async Task<IActionResult> Index()
        {
            var notificaciones = await notificacionRepository.ObtenerTodos();

            return View(notificaciones);
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}