using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Model
{
    [Table("Magazyny")]
    public partial class Magazyn
    {
        public int id { get; set; }
        public string adres { get; set; }
        public string nazwa { get; set; }
        public virtual ICollection<DostepneCzesci> czesci { get; set; }
    }
}
