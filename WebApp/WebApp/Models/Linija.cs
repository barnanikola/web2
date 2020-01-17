using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Linija
    {
        public int Id { get; set; }
        public int Broj { get; set; }
        public virtual List<Stanica> Stanice { get; set; } = new List<Stanica>();
        public string Boja { get; set; }
        public string TipLinije { get; set; }

        public Linija(Linija linija)
        {
            this.Id = linija.Id;
            this.Broj = linija.Broj;
            this.Stanice = new List<Stanica>(linija.Stanice);   
            this.Boja = linija.Boja;
            this.TipLinije = linija.TipLinije;
        }

        public Linija() { }
    }
}