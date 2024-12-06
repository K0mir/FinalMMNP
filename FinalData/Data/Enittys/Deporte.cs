using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Final.Data.Data.Entitys
{
    [Table("Deporte")]
    public class Deporte
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeporteId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantJugadores { get; set; }
        public DateTime FechaCracion { get; set; }
        public int Popularidad { get; set; }

        public int IdTipo { get; set; }
        [ForeignKey("IdTipo")]
        public TipoDeporte? TipoDeporte { get; set; }
    }
}