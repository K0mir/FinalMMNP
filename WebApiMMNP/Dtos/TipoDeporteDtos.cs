using FinalData.Data.Entitys;

namespace WebApiMMNP.Dtos
{
    public class TipoDeporteDtos
    {
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
