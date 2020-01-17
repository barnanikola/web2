using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class KartaBindingModel
    {
        public string TipKarte { get; set; }
        public double Cena { get; set; }
        public string EmailKorisnika { get; set; }

        public KartaBindingModel(KartaBindingModel karta)
        {
            this.TipKarte = karta.TipKarte;
            this.Cena = karta.Cena;
            this.EmailKorisnika = karta.EmailKorisnika;
        }

        public KartaBindingModel()
        {

        }
    }
}