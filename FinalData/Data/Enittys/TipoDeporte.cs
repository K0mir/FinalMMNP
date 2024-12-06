using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace FinalData.Data.Entitys
{
    [Table("TipoDeporte")]
    public class TipoDeporte
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public virtual List<Deporte>? Deportes { get; set; }
    }
}