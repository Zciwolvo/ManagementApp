using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApp.Models
{
    public class Kategorie
    {
        public int IDkategorii { get; set; }
        public string? NazwaKategorii { get; set; }
        public string? Opis { get; set; }
        public byte[]? Rysunek { get; set; }
    }
}
