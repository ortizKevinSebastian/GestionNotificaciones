namespace GestionNotificaciones.Models.Entities
{
    public class Notificacion
    {
        public Guid Id { get; set; }
        public string Encabezado { get; set; }
        public string TituloPagina { get; set; }
        public string Texto { get; set; }
        public string Contenido { get; set; }
        public string UrlHandle { get; set; }
        public DateTime Fecha { get; set; }
        public bool Visible { get; set; }


        //Una Notificaion puede tener multiples tags
        //Navigation property (related property) - tenerlo en cuenta para el método ObtenerTodos en el repository (Include)
        public ICollection<Tag> Tags { get; set; }
    }
}
