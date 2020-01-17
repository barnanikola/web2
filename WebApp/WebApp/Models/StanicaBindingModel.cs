using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class StanicaBindingModel
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public Position Position { get; set; }
        public List<int> Linije { get; set; }
        public int IdStanice { get; set; }

        public StanicaBindingModel() { }
    }
}