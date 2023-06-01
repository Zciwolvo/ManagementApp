using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApp.Models
{
    public class Produkty
    {
        public int IDproduktu { get; set; }
        public string NazwaProduktu { get; set; }
        public int IDdostawcy { get; set; }
        public int IDkategorii { get; set; }
        public string IlośćJednostkowa { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public int StanMagazynu { get; set; }
        public int IlośćZamówiona { get; set; }
        public int StanMinimum { get; set; }
        public bool Wycofany { get; set; }
    }

}
