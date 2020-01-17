using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Voznja
    {
        public int Id { get; set; }
        public int LinijaId { get; set; }
        public string Polazak { get; set; }

        public Voznja(Voznja voznja)
        {
            this.Id = voznja.Id;
            this.LinijaId = voznja.LinijaId;
            this.Polazak = voznja.Polazak;
        }

        public Voznja() { }
    }
}