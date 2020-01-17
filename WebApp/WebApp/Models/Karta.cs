using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Karta
    {
        public int Id { get; set; }
        public string TipKarte { get; set; }
        public double Cena { get; set; }
        public string EmailKorisnika { get; set; }
        public DateTime DatumIsteka { get; set; }
        public DateTime DatumIzdavanja { get; set; }

        public Karta(Karta karta)
        {
            this.Id = karta.Id;
            this.TipKarte = karta.TipKarte;
            this.Cena = karta.Cena;
            this.EmailKorisnika = karta.EmailKorisnika;
            this.DatumIsteka = karta.DatumIsteka;
            this.DatumIzdavanja = karta.DatumIzdavanja;
        }

        public Karta() { }
    }
}