using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApp.Models
{
    public class Zamówienia
    {
        public int IDzamówienia { get; set; }
        public int IDklienta { get; set; }
        public int IDpracownika { get; set; }
        public DateTime DataZamówienia { get; set; }
        public DateTime DataWymagana { get; set; }
        public DateTime DataWysyłki { get; set; }
        public int IDspedytora { get; set; }
        public decimal Fracht { get; set; }
        public string? NazwaOdbiorcy { get; set; }
        public string? AdresOdbiorcy { get; set; }
        public string? MiastoOdbiorcy { get; set; }
        public string? RegionOdbiorcy { get; set; }
        public string? KodPocztowyOdbiorcy { get; set; }
        public string? KrajOdbiorcy { get; set; }
    }

}
