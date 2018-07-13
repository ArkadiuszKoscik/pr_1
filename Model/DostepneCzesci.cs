using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Model
{
    [Table("DostepneCzesci")]
    public partial class DostepneCzesci
    {
        public int id { get; set; }
        public int ilosc { get; set; }
        public virtual Magazyn idMagazynu { get; set; }
        public virtual Czesc idCzesci { get; set; }
    }
}
