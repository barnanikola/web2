namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if(false)
            {
                if (!context.Roles.Any(r => r.Name == "Admin"))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole { Name = "Admin" };

                    manager.Create(role);
                }

                if (!context.Roles.Any(r => r.Name == "Controller"))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole { Name = "Controller" };

                    manager.Create(role);
                }

                if (!context.Roles.Any(r => r.Name == "AppUser"))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole { Name = "AppUser" };

                    manager.Create(role);
                }

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
                {
                    var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("admin123"), Adresa = "Novosadska 120", DatRodj = new DateTime(1993, 11, 18), Prezime = "Barna", TipKorisnika = "Obican", VrstaNaloga = "Admin", Ime = "Nikola" };
                    userManager.Create(user);
                    userManager.AddToRole(user.Id, "Admin");
                }

                if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
                {
                    var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("korisnik123"), Adresa = "Kosovska 18", DatRodj = new DateTime(1995, 8, 15), Prezime = "Markovic", TipKorisnika = "Obican", VrstaNaloga = "Korisnik", Ime = "Marko" };
                    userManager.Create(user);
                    userManager.AddToRole(user.Id, "AppUser");
                }

                if (!context.Users.Any(u => u.UserName == "kontroler@yahoo.com"))
                {
                    var user = new ApplicationUser() { Id = "kontroler", UserName = "kontroler@yahoo", Email = "kontroler@yahoo.com", PasswordHash = ApplicationUser.HashPassword("kontroler123"), Adresa = "Kosovska 18", DatRodj = new DateTime(1985, 8, 15), Prezime = "Milos", TipKorisnika = "Obican", VrstaNaloga = "Korisnik", Ime = "Milosevic" };
                    userManager.Create(user);
                    userManager.AddToRole(user.Id, "Controller");
                }

                if (!context.Cenovnici.Any())
                {
                    Cenovnik cenovnik = new Cenovnik()
                    {
                        Id = 1,
                        VaziOd = new DateTime(2019, 12, 1),
                        VaziDo = new DateTime(2020, 2, 1),
                        CenaVremenskeKarte = 50,
                        CenaDnevneKarte = 150,
                        CenaMesecneKarte = 1500,
                        CenaGodisnjeKarte = 15000
                    };
                    context.Cenovnici.AddOrUpdate(cenovnik);
                    context.SaveChanges();
                }


                if (true)
                {
                    Stanica stanica1 = new Stanica() { Id = 1, Adresa = "Bulevar Jase Tomica 52", Naziv = "Zeleznicka stanica", Position = new Position(45.264336, 19.831434) };
                    Stanica stanica2 = new Stanica() { Id = 2, Adresa = "Kisacka 12", Naziv = "Nadvoznjak", Position = new Position(45.266228, 19.835397) };
                    Stanica stanica3 = new Stanica() { Id = 3, Adresa = "Kisacka 25", Naziv = "Industrijska zona", Position = new Position(45.269006, 19.831513) };
                    Stanica stanica4 = new Stanica() { Id = 4, Adresa = "Partizanska 32", Naziv = "Radna zona sever", Position = new Position(45.270836, 19.842414) };
                    Stanica stanica5 = new Stanica() { Id = 5, Adresa = "Temerinska 58", Naziv = "FK CZ", Position = new Position(45.283840, 19.834554) };
                    Stanica stanica6 = new Stanica() { Id = 6, Adresa = "Temerinska 35", Naziv = "Majke Jugovica", Position = new Position(45.290507, 19.831213) };
                    Stanica stanica7 = new Stanica() { Id = 7, Adresa = "Put 102", Naziv = "Novi Sad Sever", Position = new Position(45.310771, 19.831212) };
                    Stanica stanica8 = new Stanica() { Id = 8, Adresa = "Put 102", Naziv = "Ktm Trans", Position = new Position(45.320747, 19.837319) };
                    Stanica stanica9 = new Stanica() { Id = 9, Adresa = "Novosadska 58", Naziv = "Backi Jarak", Position = new Position(45.374679, 19.875274) };
                    Stanica stanica10 = new Stanica() { Id = 10, Adresa = "Novosadska 200", Naziv = "Temerin", Position = new Position(45.414609, 19.891594) };
                    Stanica cenej = new Stanica() { Id = 11, Adresa = "Cenejska", Naziv = "Cenej", Position = new Position(45.338286, 19.830405) };
                    Stanica banatic = new Stanica() { Id = 12, Adresa = "Rumenacka 20", Naziv = "Banatic", Position = new Position(45.263362, 19.818592) };
                    Stanica groblje = new Stanica() { Id = 13, Adresa = "Rumenacki put 58", Naziv = "Novo groblje", Position = new Position(45.278315, 19.793882) };
                    Stanica rumenka = new Stanica() { Id = 14, Adresa = "Partizanska 65", Naziv = "Rumenka", Position = new Position(45.294107, 19.738444) };
                    Stanica buoOslBulKP = new Stanica() { Id = 15, Adresa = "Bulevar oslobodjenja 41", Naziv = "Bulevarri", Position = new Position(45.260504, 19.832770) };
                    Stanica futoskaPijaca = new Stanica() { Id = 16, Adresa = "Bulevar oslobodjenja 59", Naziv = "Futoska pijaca", Position = new Position(45.251801, 19.837494) };
                    Stanica futoskiPark = new Stanica() { Id = 17, Adresa = "Futoska 38", Naziv = "Futoski park", Position = new Position(45.248972, 19.830034) };
                    Stanica klinicki = new Stanica() { Id = 18, Adresa = "Futoska 45", Naziv = "Klinicki centar", Position = new Position(45.249634, 19.824151) };
                    Stanica mcPoliklinika = new Stanica() { Id = 19, Adresa = "Cara Dusana 79", Naziv = "MC Poliklinika", Position = new Position(45.240353, 19.825663) };
                    Stanica bulPathPavla = new Stanica() { Id = 20, Adresa = "Bulevar Patrijaha Pavla 1", Naziv = "Telep", Position = new Position(45.241967, 19.796143) };
                    Stanica big = new Stanica() { Id = 21, Adresa = "Sentandrejski put 11", Naziv = "Trzni centar BIG", Position = new Position(45.275868, 19.829917) };
                    Stanica trgMarije = new Stanica() { Id = 22, Adresa = "Kisacka 1", Naziv = "Trg Marije Trandafil", Position = new Position(45.260863, 19.842753) };
                    Stanica snp = new Stanica() { Id = 23, Adresa = "Uspenska 1", Naziv = "Srpsko narodno pozoriste", Position = new Position(45.254613, 19.841896) };
                    Stanica thePub = new Stanica() { Id = 24, Adresa = "Bulever Oslobodjenja 113", Naziv = "Stadion", Position = new Position(45.247796, 19.839617) };
                    Stanica merkator = new Stanica() { Id = 25, Adresa = "Bulevar Oslobodjenja 119", Naziv = "Merkator", Position = new Position(45.244087, 19.841604) };
                    Stanica limanskiPark = new Stanica() { Id = 26, Adresa = "Bulevar Oslobodjenja 133", Naziv = "Limanski park", Position = new Position(45.241764, 19.842886) };
                    Stanica liman = new Stanica() { Id = 27, Adresa = "Narodnog fronta 79", Naziv = "Liman", Position = new Position(45.237344, 19.826703) };
                    Stanica zavod = new Stanica() { Id = 28, Adresa = "Hajduk veljkova 15", Naziv = "Zavod za transfuziju krvi", Position = new Position(45.254035, 19.824078) };
                    Stanica sajam = new Stanica() { Id = 29, Adresa = "HajdukVeljkova 4", Naziv = "Sajam", Position = new Position(45.258233, 19.824053) };
                    Stanica spens = new Stanica() { Id = 30, Adresa = "Bulevar cara lazara 78", Naziv = "Spens", Position = new Position(45.245302, 19.847209) };
                    Stanica univerzitet = new Stanica() { Id = 31, Adresa = "Bulevar Cara Lazara 98", Naziv = "Studentski grad", Position = new Position(45.248061, 19.849770) };
                    Stanica apr = new Stanica() { Id = 32, Adresa = "Vojvodjanskih brigada 18", Naziv = "APR", Position = new Position(45.251483, 19.846916) };
                    Stanica vlada = new Stanica() { Id = 33, Adresa = "Bulevar Mihajla Pupina 16", Naziv = "Vlada", Position = new Position(45.253758, 19.847849) };
                    Stanica most = new Stanica() { Id = 34, Adresa = "Trg neznanog junaka", Naziv = "Dugin most", Position = new Position(45.254186, 19.853918) };
                    Stanica beogradska = new Stanica() { Id = 35, Adresa = "Beogradska 8", Naziv = "Petrovaradin", Position = new Position(45.254980, 19.861630) };

                    Linija linija1 = new Linija() { Id = 1, Broj = 1, Boja = "#0000FF", TipLinije = "Gradska" };
                    linija1.Stanice.Add(stanica1);
                    linija1.Stanice.Add(buoOslBulKP);
                    linija1.Stanice.Add(futoskaPijaca);
                    linija1.Stanice.Add(futoskiPark);
                    linija1.Stanice.Add(klinicki);
                    linija1.Stanice.Add(mcPoliklinika);
                    linija1.Stanice.Add(bulPathPavla);
                    context.Linije.AddOrUpdate(linija1);
                    context.SaveChanges();

                    Linija linija2 = new Linija() { Id = 2, Broj = 2, Boja = "#00BBFF", TipLinije = "Gradska" };
                    linija2.Stanice.Add(big);
                    linija2.Stanice.Add(stanica3);
                    linija2.Stanice.Add(stanica2);
                    linija2.Stanice.Add(trgMarije);
                    linija2.Stanice.Add(snp);
                    linija2.Stanice.Add(futoskaPijaca);
                    linija2.Stanice.Add(thePub);
                    linija2.Stanice.Add(merkator);
                    linija2.Stanice.Add(limanskiPark);
                    linija2.Stanice.Add(liman);
                    context.Linije.AddOrUpdate(linija2);
                    context.SaveChanges();

                    Linija linija3 = new Linija() { Id = 3, Broj = 3, Boja = "#00FF77", TipLinije = "Gradska" };
                    linija3.Stanice.Add(snp);
                    linija3.Stanice.Add(futoskaPijaca);
                    linija3.Stanice.Add(futoskiPark);
                    linija3.Stanice.Add(klinicki);
                    linija3.Stanice.Add(zavod);
                    linija3.Stanice.Add(sajam);
                    linija3.Stanice.Add(banatic);
                    linija3.Stanice.Add(groblje);
                    context.Linije.AddOrUpdate(linija3);
                    context.SaveChanges();

                    Linija linija4 = new Linija() { Id = 4, Broj = 4, Boja = "#FF6600", TipLinije = "Gradska" };
                    linija4.Stanice.Add(liman);
                    linija4.Stanice.Add(limanskiPark);
                    linija4.Stanice.Add(merkator);
                    linija4.Stanice.Add(spens);
                    linija4.Stanice.Add(univerzitet);
                    linija4.Stanice.Add(apr);
                    linija4.Stanice.Add(vlada);
                    linija4.Stanice.Add(most);
                    linija4.Stanice.Add(beogradska);
                    context.Linije.AddOrUpdate(linija4);
                    context.SaveChanges();

                    Linija linija32 = new Linija() { Id = 5, Broj = 32, Boja = "#FF2200", TipLinije = "Prigradska" };
                    linija32.Stanice.Add(stanica1);
                    linija32.Stanice.Add(stanica2);
                    linija32.Stanice.Add(stanica3);
                    linija32.Stanice.Add(stanica4);
                    linija32.Stanice.Add(stanica5);
                    linija32.Stanice.Add(stanica6);
                    linija32.Stanice.Add(stanica7);
                    linija32.Stanice.Add(stanica8);
                    linija32.Stanice.Add(stanica9);
                    linija32.Stanice.Add(stanica10);
                    context.Linije.AddOrUpdate(linija32);
                    context.SaveChanges();

                    Linija linija30 = new Linija() { Id = 6, Broj = 30, Boja = "#FF00DD", TipLinije = "Prigradska" };
                    linija30.Stanice.Add(stanica1);
                    linija30.Stanice.Add(stanica2);
                    linija30.Stanice.Add(stanica3);
                    linija30.Stanice.Add(stanica4);
                    linija30.Stanice.Add(stanica5);
                    linija30.Stanice.Add(stanica6);
                    linija30.Stanice.Add(stanica7);
                    linija30.Stanice.Add(cenej);
                    context.Linije.AddOrUpdate(linija30);
                    context.SaveChanges();

                    Linija linij28 = new Linija() { Id = 7, Broj = 28, Boja = "#AA00FF", TipLinije = "Prigradska" };
                    linij28.Stanice.Add(stanica1);
                    linij28.Stanice.Add(banatic);
                    linij28.Stanice.Add(groblje);
                    linij28.Stanice.Add(rumenka);
                    context.Linije.AddOrUpdate(linij28);
                    context.SaveChanges();

                    stanica1.Linije.Add(linija32);
                    stanica1.Linije.Add(linija30);
                    stanica1.Linije.Add(linij28);
                    stanica1.Linije.Add(linija1);
                    context.Stanice.AddOrUpdate(stanica1);
                    context.SaveChanges();

                    stanica2.Linije.Add(linija32);
                    stanica2.Linije.Add(linija30);
                    stanica2.Linije.Add(linija2);
                    context.Stanice.AddOrUpdate(stanica2);
                    context.SaveChanges();

                    stanica3.Linije.Add(linija30);
                    stanica3.Linije.Add(linija32);
                    stanica3.Linije.Add(linija2);
                    context.Stanice.AddOrUpdate(stanica3);
                    context.SaveChanges();

                    stanica4.Linije.Add(linija30);
                    stanica4.Linije.Add(linija32);
                    context.Stanice.AddOrUpdate(stanica4);
                    context.SaveChanges();

                    stanica5.Linije.Add(linija32);
                    stanica5.Linije.Add(linija30);
                    context.Stanice.AddOrUpdate(stanica5);
                    context.SaveChanges();

                    stanica6.Linije.Add(linija30);
                    stanica6.Linije.Add(linija32);
                    context.Stanice.AddOrUpdate(stanica6);
                    context.SaveChanges();

                    stanica7.Linije.Add(linija32);
                    stanica7.Linije.Add(linija30);
                    context.Stanice.AddOrUpdate(stanica7);
                    context.SaveChanges();

                    stanica8.Linije.Add(linija32);
                    context.Stanice.AddOrUpdate(stanica8);
                    context.SaveChanges();

                    stanica9.Linije.Add(linija32);
                    context.Stanice.AddOrUpdate(stanica9);
                    context.SaveChanges();

                    stanica10.Linije.Add(linija32);
                    context.Stanice.AddOrUpdate(stanica10);
                    context.SaveChanges();

                    cenej.Linije.Add(linija30);
                    context.Stanice.AddOrUpdate(cenej);
                    context.SaveChanges();

                    banatic.Linije.Add(linij28);
                    banatic.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(banatic);
                    context.SaveChanges();

                    groblje.Linije.Add(linij28);
                    groblje.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(groblje);
                    context.SaveChanges();

                    rumenka.Linije.Add(linij28);
                    context.Stanice.AddOrUpdate(rumenka);
                    context.SaveChanges();

                    buoOslBulKP.Linije.Add(linija1);
                    context.Stanice.AddOrUpdate(buoOslBulKP);
                    context.SaveChanges();

                    futoskaPijaca.Linije.Add(linija1);
                    futoskaPijaca.Linije.Add(linija2);
                    futoskaPijaca.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(futoskaPijaca);
                    context.SaveChanges();

                    futoskiPark.Linije.Add(linija1);
                    futoskiPark.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(futoskiPark);
                    context.SaveChanges();

                    klinicki.Linije.Add(linija1);
                    klinicki.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(klinicki);
                    context.SaveChanges();

                    mcPoliklinika.Linije.Add(linija1);
                    context.Stanice.AddOrUpdate(mcPoliklinika);
                    context.SaveChanges();

                    bulPathPavla.Linije.Add(linija1);
                    context.Stanice.AddOrUpdate(bulPathPavla);
                    context.SaveChanges();

                    big.Linije.Add(linija2);
                    context.Stanice.AddOrUpdate(big);
                    context.SaveChanges();

                    trgMarije.Linije.Add(linija2);
                    context.Stanice.AddOrUpdate(trgMarije);
                    context.SaveChanges();

                    snp.Linije.Add(linija2);
                    snp.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(snp);
                    context.SaveChanges();

                    thePub.Linije.Add(linija2);
                    context.Stanice.AddOrUpdate(thePub);
                    context.SaveChanges();

                    merkator.Linije.Add(linija2);
                    merkator.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(merkator);
                    context.SaveChanges();

                    limanskiPark.Linije.Add(linija2);
                    limanskiPark.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(limanskiPark);
                    context.SaveChanges();

                    liman.Linije.Add(linija2);
                    liman.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(liman);
                    context.SaveChanges();

                    zavod.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(zavod);
                    context.SaveChanges();

                    sajam.Linije.Add(linija3);
                    context.Stanice.AddOrUpdate(sajam);
                    context.SaveChanges();

                    spens.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(spens);
                    context.SaveChanges();

                    univerzitet.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(univerzitet);
                    context.SaveChanges();

                    apr.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(apr);
                    context.SaveChanges();

                    vlada.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(vlada);
                    context.SaveChanges();

                    most.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(most);
                    context.SaveChanges();

                    beogradska.Linije.Add(linija4);
                    context.Stanice.AddOrUpdate(beogradska);
                    context.SaveChanges();
                }


                int idVoznje = 1;
                for (int i = 1; i < 4; i++)
                {
                    RedVoznje redVoznje = new RedVoznje();
                    redVoznje.Id = i;
                    redVoznje.LinijaId = i;
                    redVoznje.Dan = "Radni";
                    redVoznje.Polasci = new List<Voznja>();

                    for (int j = 6; j < 24; j++)
                    {
                        Voznja voznja = new Voznja();
                        voznja.Id = idVoznje;
                        voznja.LinijaId = i;
                        voznja.Polazak = String.Format("{0} h", j);
                        redVoznje.Polasci.Add(voznja);
                        context.Voznje.AddOrUpdate(voznja);
                        context.SaveChanges();
                        idVoznje++;

                        Voznja voznja2 = new Voznja();
                        voznja2.Id = idVoznje;
                        voznja2.LinijaId = i;
                        voznja2.Polazak = String.Format("{0} h : 30 min", j);
                        redVoznje.Polasci.Add(voznja2);
                        context.Voznje.AddOrUpdate(voznja2);
                        context.SaveChanges();
                        idVoznje++;
                    }
                    context.RedoviVoznje.AddOrUpdate(redVoznje);
                    context.SaveChanges();
                }

                for (int i = 4; i < 7; i++)
                {
                    RedVoznje redVoznje = new RedVoznje();
                    redVoznje.Id = i;
                    redVoznje.LinijaId = i - 3;
                    redVoznje.Dan = "Subota";
                    redVoznje.Polasci = new List<Voznja>();

                    for (int j = 6; j < 24; j++)
                    {
                        Voznja voznja = new Voznja();
                        voznja.Id = idVoznje;
                        voznja.LinijaId = i - 3;
                        voznja.Polazak = String.Format("{0} h : 00 min", j);
                        redVoznje.Polasci.Add(voznja);
                        context.Voznje.AddOrUpdate(voznja);
                        context.SaveChanges();
                        idVoznje++;
                    }

                    context.RedoviVoznje.AddOrUpdate(redVoznje);
                    context.SaveChanges();
                }

                for (int i = 7; i < 10; i++)
                {
                    RedVoznje redVoznje = new RedVoznje();
                    redVoznje.Id = i;
                    redVoznje.LinijaId = i - 6;
                    redVoznje.Dan = "Nedelja";
                    redVoznje.Polasci = new List<Voznja>();

                    for (int j = 6; j < 24; j += 2)
                    {
                        Voznja voznja = new Voznja();
                        voznja.Id = idVoznje;
                        voznja.LinijaId = i - 6;
                        voznja.Polazak = String.Format("{0} h : 00 min", j);
                        redVoznje.Polasci.Add(voznja);
                        context.Voznje.AddOrUpdate(voznja);
                        context.SaveChanges();
                        idVoznje++;
                    }
                    context.RedoviVoznje.AddOrUpdate(redVoznje);
                    context.SaveChanges();
                }

                for (int i = 11; i <= 13; i++)
                {
                    RedVoznje redVoznje = new RedVoznje();
                    redVoznje.Id = i;
                    redVoznje.LinijaId = i - 6;
                    redVoznje.Dan = "Radni";
                    redVoznje.Polasci = new List<Voznja>();

                    for (int j = 6; j < 24; j++)
                    {
                        Voznja voznja = new Voznja();
                        voznja.Id = idVoznje;
                        voznja.LinijaId = i - 6;
                        voznja.Polazak = String.Format("{0} h : 00 min", j);
                        redVoznje.Polasci.Add(voznja);
                        context.Voznje.AddOrUpdate(voznja);
                        context.SaveChanges();
                        idVoznje++;
                    }
                    context.RedoviVoznje.AddOrUpdate(redVoznje);
                    context.SaveChanges();
                }

                for (int i = 14; i <= 16; i++)
                {
                    RedVoznje redVoznje = new RedVoznje();
                    redVoznje.Id = i;
                    redVoznje.LinijaId = i - 9;
                    redVoznje.Dan = "Subota";
                    redVoznje.Polasci = new List<Voznja>();

                    for (int j = 6; j < 24; j += 2)
                    {
                        Voznja voznja = new Voznja();
                        voznja.Id = idVoznje;
                        voznja.LinijaId = i - 9;
                        voznja.Polazak = String.Format("{0} h : 00 min", j);
                        redVoznje.Polasci.Add(voznja);
                        context.Voznje.AddOrUpdate(voznja);
                        context.SaveChanges();
                        idVoznje++;
                    }
                    context.RedoviVoznje.AddOrUpdate(redVoznje);
                    context.SaveChanges();
                }

                for (int i = 17; i <= 19; i++)
                {
                    RedVoznje redVoznje = new RedVoznje();
                    redVoznje.Id = i;
                    redVoznje.LinijaId = i - 12;
                    redVoznje.Dan = "Nedelja";
                    redVoznje.Polasci = new List<Voznja>();

                    for (int j = 6; j < 24; j += 3)
                    {
                        Voznja voznja = new Voznja();
                        voznja.Id = idVoznje;
                        voznja.LinijaId = i - 12;
                        voznja.Polazak = String.Format("{0} h : 00 min", j);
                        redVoznje.Polasci.Add(voznja);
                        context.Voznje.AddOrUpdate(voznja);
                        context.SaveChanges();
                        idVoznje++;
                    }
                    context.RedoviVoznje.AddOrUpdate(redVoznje);
                    context.SaveChanges();
                }
            }
        }   
    }
}
