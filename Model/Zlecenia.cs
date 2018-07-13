using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Model
{
    [Table("Zlecenia")]
    public partial class Zlecenie
    {
        public int id { get; set; }
        public string opisUsterki { get; set; }
        public string opisNaprawy { get; set; }
        public DateTime dataZlecenia { get; set; }
        public decimal koszt { get; set; }
        public virtual Mechanik idMechanika { get; set; }
        public virtual Auto idAuta { get; set; }
        public virtual ICollection<PotrzebnaCzesc> czesciDoNaprawy { get; set; }
    }
}
