using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stanica
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public Position Position { get; set; }
        public virtual List<Linija> Linije { get; set; } = new List<Linija>();

        public Stanica(Stanica stanica)
        {
            this.Id = stanica.Id;
            this.Naziv = stanica.Naziv;
            this.Adresa = stanica.Adresa;
            this.Position = stanica.Position;
            this.Linije = new List<Linija>(stanica.Linije);
        }

        public Stanica() { }
    }
}