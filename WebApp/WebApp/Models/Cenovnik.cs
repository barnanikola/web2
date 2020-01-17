using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Cenovnik
    {
        public int Id { get; set; }
        public DateTime VaziOd { get; set; }
        public DateTime VaziDo { get; set; }
        public double CenaVremenskeKarte { get; set; }
        public double CenaDnevneKarte { get; set; }
        public double CenaMesecneKarte { get; set; }
        public double CenaGodisnjeKarte { get; set; }

        public Cenovnik(Cenovnik cenovnik)
        {
            this.Id = cenovnik.Id;
            this.VaziOd = cenovnik.VaziOd;
            this.VaziDo = cenovnik.VaziDo;
            this.CenaVremenskeKarte = cenovnik.CenaVremenskeKarte;
            this.CenaDnevneKarte = cenovnik.CenaDnevneKarte;
            this.CenaMesecneKarte = cenovnik.CenaMesecneKarte;
            this.CenaGodisnjeKarte = cenovnik.CenaGodisnjeKarte;
        }

        public Cenovnik() { }
    }
}