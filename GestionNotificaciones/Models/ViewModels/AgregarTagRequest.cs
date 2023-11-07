using static GestionNotificaciones.Validation.Validaciones;

namespace GestionNotificaciones.Models.ViewModels
{
    public class AgregarTagRequest
    {
        [Requerido]
        public string Nombre { get; set; }

        [Requerido]
        public string Descripcion { get; set; }
    }
}
