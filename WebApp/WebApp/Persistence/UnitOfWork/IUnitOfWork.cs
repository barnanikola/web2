using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICenovnikRepository Cenovnici { get; set; }
        IKartaRepository Karte { get; set; }
        ILinijaRepository Linije { get; set; }
        IRedVoznjeRepository RedoviVoznje { get; set; }
        IStanicaRepository Stanice { get; set; }
        IVoznjaRepository Voznje { get; set; }

        int Complete();
    }
}
