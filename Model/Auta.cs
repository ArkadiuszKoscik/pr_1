using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Model
{
    [Table("Auta")]
    public partial class Auto
    {
        public int id { get; set; }
        public string marka { get; set; }
        public string model { get; set; }
        public int rocznik { get; set; }
        public string vin { get; set; }
        public virtual Klient idKlienta { get; set; }
        public virtual ICollection<Zlecenie> zlecenia { get; set; }
    }
}
