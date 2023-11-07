using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionNotificaciones.Models.ViewModels
{
    public class AgregarNotifRequest
    {
        public string Encabezado { get; set; }
        public string TituloPagina { get; set; }
        public string Texto { get; set; }
        public string Contenido { get; set; }
        public string UrlHandle { get; set; }
        public DateTime Fecha { get; set; }
        public bool Visible { get; set; }


        //propiedades para mostrar y acceder a los tags
        //para mostrar los tags (en la View, en el dropdown/list box)
        public IEnumerable<SelectListItem> Tags { get; set; }

        //para almacenarlos (Post a la Db con el bt Submit)
        public string[] Selecionado { get; set; } = Array.Empty<string>();
    }
}
