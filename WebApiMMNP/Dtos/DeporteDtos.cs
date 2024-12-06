namespace WebApiMMNP.Dtos
{
    public class DeporteDtos
    {
        public int DeporteId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantJugadores { get; set; }
        public DateTime FechaCracion { get; set; }
        public int Popularidad { get; set; }
        public int IdTipo { get; set; }
        public string NombreTipo {  get; set; }
    }
}
