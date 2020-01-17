using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class VoznjaRepository : Repository<Voznja, int>, IVoznjaRepository
    {
        public VoznjaRepository(DbContext context) : base(context) { }
    }
}