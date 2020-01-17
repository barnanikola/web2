using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RedVoznje
    {
        public int Id { get; set; }
        public string Dan { get; set; }
        public virtual List<Voznja> Polasci { get; set; } = new List<Voznja>();
        public int LinijaId { get; set; }

        public RedVoznje(RedVoznje redVoznje)
        {
            this.Id = redVoznje.Id;
            this.Dan = redVoznje.Dan;
            this.Polasci = new List<Voznja>(redVoznje.Polasci);
            this.LinijaId = redVoznje.LinijaId;
        }

        public RedVoznje() { }
    }
}