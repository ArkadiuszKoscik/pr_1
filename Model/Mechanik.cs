using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Model
{
    [Table("Mechanicy")]
    public partial class Mechanik
    {
        public int id { get; set; }
        public string imie { get; set; }
        public string  nazwisko { get; set; }
        public decimal pensja { get; set; }
        public virtual ICollection<Zlecenie> zlecenia { get; set; }
    }
}
