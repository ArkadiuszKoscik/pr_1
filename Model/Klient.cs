using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Model
{
    [Table("Klienci")]
    public partial class Klient
    {
        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string telefon { get; set; }
        public virtual ICollection<Auto> pojazdy { get; set; }
    }
}
