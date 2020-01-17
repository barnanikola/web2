using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class LinijaNumberBindingModel
    {
        public int Id { get; set; }
        public int Broj { get; set; }

        public LinijaNumberBindingModel() { }
        public LinijaNumberBindingModel(Linija linija)
        {
            Id = linija.Id;
            Broj = linija.Broj;
        }
    }
}