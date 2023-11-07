using static GestionNotificaciones.Validation.Validaciones;

namespace GestionNotificaciones.Models.ViewModels
{
    public class EditarTagRequest
    {
        public Guid Id { get; set; }

        [Requerido]
        public string Nombre { get; set; }

        [Requerido]
        public string Descripcion { get; set; }
    }
}
