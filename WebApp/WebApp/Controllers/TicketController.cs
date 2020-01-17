using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Ticket")]
    public class TicketController : ApiController
    {
        private IUnitOfWork db;
        private ApplicationUserManager _userManager;

        public TicketController(IUnitOfWork context)
        {
            db = context;
        }

        public TicketController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [Route("GetTickets")]
        public IEnumerable<Karta> GetTickets()
        {
            List<Karta> karte = new List<Karta>(db.Karte.GetAll());

            return karte;
        }

        [Route("BuyTicket")]
        public IHttpActionResult BuyTicket(KartaBindingModel model)
        {
            if(model.EmailKorisnika == "undefined" && model.TipKarte!="vremenska")
            {
                return BadRequest("Morate biti ulogovani da bi kupili tu kartu");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.EmailKorisnika != "undefined")
            {
                ApplicationUser user = UserManager.FindByEmail(model.EmailKorisnika);
                if (user.Status != "zahtev je prihvacen")
                {
                    return BadRequest("ne odgovarajuci status korisnika");
                }
            }
            
            DateTime sada = DateTime.Now;
            DateTime datumIzdavanja = sada;
            DateTime datumIsteka = sada;
            switch (model.TipKarte)
            {
                case "vremenska": datumIsteka = new DateTime(sada.Year, sada.Month, sada.Day, sada.Hour + 1, sada.Minute, sada.Second);
                    break;
                case "dnevna": datumIsteka = new DateTime(sada.Year, sada.Month, sada.AddDays(1).Day, 0, 0, 0);
                    break;
                case "mesecna": datumIsteka = new DateTime(sada.Year, sada.AddMonths(1).Month, 1, 0, 0, 0);
                    break;
                case "godisnja": datumIsteka = new DateTime(sada.AddYears(1).Year, 1, 1, 0, 0, 0);
                    break;
                default: datumIsteka = DateTime.Now;
                    break;
            }
            Karta karta = new Karta()
            {
                Cena = model.Cena,
                EmailKorisnika = model.EmailKorisnika,
                TipKarte = model.TipKarte,
                DatumIzdavanja = datumIzdavanja,
                DatumIsteka = datumIsteka
            };
            try
            {
                db.Karte.Add(karta);
                db.Complete();
            }
            catch
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            
            return Ok();
        }

        [Route("GetPriceList")]
        public IHttpActionResult GetPriceList()
        {
            List<Cenovnik> cenovnici = new List<Cenovnik>(db.Cenovnici.GetAll());
            DateTime sad = DateTime.Now;
            foreach(Cenovnik cenovnik in cenovnici)
            {
                if(cenovnik.VaziOd<=sad && sad <= cenovnik.VaziDo)
                {
                    return Ok(cenovnik);
                }
            }
            return BadRequest("Cenovnik jos nije napravljen");
        }

        [AcceptVerbs("GET", "POST")]
        [Route("ChekValidity")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult ChekValidity(int id)
        {
            List<Karta> karte = new List<Karta>(db.Karte.GetAll());
            foreach (Karta k in karte)
            {
                if(k.Id == id)
                {
                    return Ok(ChekTicket(k));
                }
            }

            return BadRequest("Karta sa Id brojem " + id + " ne postoji");
        }

        private string ChekTicket(Karta karta)
        {
            DateTime sad = DateTime.Now;
            if (karta.DatumIsteka < sad)
            {
                return "Karta sa Id brojem " + karta.Id + " nije validna";
            }
            return "Karta sa Id brojem " + karta.Id + " jeste validna";
        }

        [Route("AddCenovnik")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddCenovnik (Cenovnik cenovnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(cenovnik.Id != 0)
            {
                try
                {
                    db.Cenovnici.Update(cenovnik);
                    db.Complete();
                }
                catch (Exception e)
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
                
            }
            else
            {
                try
                {
                    db.Cenovnici.Add(cenovnik);
                    db.Complete();
                }
                catch (Exception e)
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
            }
            
            return Ok();
        }
    }
}
