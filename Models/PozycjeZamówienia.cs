using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApp.Models
{
    public class PozycjeZamówienia
    {
        public int IDzamówienia { get; set; }
        public int IDproduktu { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public int Ilość { get; set; }
        public float Rabat { get; set; }
    }

}
