using GestionNotificaciones.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace GestionNotificaciones.Validation
{
    public class Validaciones
    {
        public class RequeridoAttribute : RequiredAttribute
        {
            public RequeridoAttribute()
            {
                this.ErrorMessage = "Campo Obligatorio";
            }
        }


        public static bool CampoVacioAgregar(AgregarNotifRequest request)
        {
            //usando LINQ
            return new string[]
            {
                request.Encabezado,
                request.TituloPagina,
                request.Texto,
                request.Contenido,
                request.UrlHandle
            }
            .Any(string.IsNullOrWhiteSpace);
        }


        public static bool CampoVacioEditar(EditarNotifRequest request) 
        {
            //usando operadores lógicos
            return string.IsNullOrWhiteSpace(request.Encabezado) ||
            string.IsNullOrWhiteSpace(request.TituloPagina) ||
            string.IsNullOrWhiteSpace(request.Texto) ||
            string.IsNullOrWhiteSpace(request.Contenido) ||
            string.IsNullOrWhiteSpace(request.UrlHandle);
        }      
    }
}
