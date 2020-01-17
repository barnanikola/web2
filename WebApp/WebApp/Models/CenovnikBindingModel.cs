using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CenovnikBindingModel
    {
        public DateTime VaziOd { get; set; }
        public DateTime VaziDo { get; set; }
        public double CenaVremenskeKarte { get; set; }
        public double CenaDnevneKarte { get; set; }
        public double CenaMesecneKarte { get; set; }
        public double CenaGodisnjeKarte { get; set; }
    }
}