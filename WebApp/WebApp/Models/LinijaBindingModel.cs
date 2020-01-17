using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class LinijaBindingModel
    {
        public int Broj { get; set; }
        public List<int> Stanice { get; set; }
        public string Boja { get; set; }
        public string TipLinije { get; set; }
        public int IdLinije { get; set; }

        public LinijaBindingModel() { }
    }
}