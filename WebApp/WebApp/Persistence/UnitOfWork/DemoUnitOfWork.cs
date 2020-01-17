using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        [Dependency]
        public ICenovnikRepository Cenovnici { get; set; }
        [Dependency]
        public IKartaRepository Karte { get; set; }
        [Dependency]
        public ILinijaRepository Linije { get; set; }
        [Dependency]
        public IRedVoznjeRepository RedoviVoznje { get; set; }
        [Dependency]
        public IStanicaRepository Stanice { get; set; }
        [Dependency]
        public IVoznjaRepository Voznje { get; set; }

        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}