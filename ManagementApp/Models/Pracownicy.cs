using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApp.Models
{
    public class Pracownicy
    {
        public int IDpracownika { get; set; }
        public string Nazwisko { get; set; }
        public string Imię { get; set; }
        public string Stanowisko { get; set; }
        public string ZwrotGrzecznościowy { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public DateTime DataZatrudnienia { get; set; }
        public string Adres { get; set; }
        public string Miasto { get; set; }
        public string Region { get; set; }
        public string KodPocztowy { get; set; }
        public string Kraj { get; set; }
        public string TelefonDomowy { get; set; }
        public string TelefonWewnętrzny { get; set; }
        public byte[] Fotografia { get; set; }
        public string Uwagi { get; set; }
        int Szef { get; set; }
    }

}
