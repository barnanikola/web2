using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Cenovnik> Cenovnici { get; set; }
        public DbSet<Karta> Karte { get; set; }
        public DbSet<Linija> Linije { get; set; }
        public DbSet<RedVoznje> RedoviVoznje { get; set; }
        public DbSet<Stanica> Stanice { get; set; }
        public DbSet<Voznja> Voznje { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}