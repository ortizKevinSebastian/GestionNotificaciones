namespace GestionNotificaciones.Models.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        //Un Tag puede tener multiples Notificaciones
        public ICollection<Notificacion> Notificaciones { get; set; }
    }
}
