using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class VoznjaBindingModel
    {
        public int IdLinije { get; set; }
        public string Dan { get; set; }
        public string Polazak { get; set; }

        public VoznjaBindingModel() {}
    }
}