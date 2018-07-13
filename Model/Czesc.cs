using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Model
{
    [Table("Czesci")]
    public partial class Czesc
    {
        public int id { get; set; }
        public string nazwa { get; set; }
        public string opis { get; set; }
        public decimal cenaCzesci { get; set; }
        public virtual ICollection<DostepneCzesci> gdzieDostepne { get; set; }
        public virtual ICollection<PotrzebnaCzesc> gdziePotrzebne { get; set; }
    }
}
