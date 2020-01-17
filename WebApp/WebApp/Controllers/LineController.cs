using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Line")]
    public class LineController : ApiController
    {
        private IUnitOfWork db;
        public LineController(IUnitOfWork context)
        {
            db = context;
        }

        [Route("GetLinije")]
        public IEnumerable<Linija> GetLinije()
        {
            List<Linija> linije = new List<Linija>(db.Linije.GetAll());
            return linije;
        }

        [Route("GetStanice")]
        public IEnumerable<Stanica> GetStanice()
        {
            List<Stanica> stanice = new List<Stanica>(db.Stanice.GetAll());

            return stanice;
        }

        [Route("GetLinijeTip")]
        public IEnumerable<LinijaNumberBindingModel> GetLinijeTip(string tip)
        {
            List<Linija> linije = new List<Linija>(db.Linije.GetAll());
            List<LinijaNumberBindingModel> returnList = new List<LinijaNumberBindingModel>();
            foreach(Linija linija in linije)
            {
                if(linija.TipLinije == tip)
                {
                    returnList.Add(new LinijaNumberBindingModel(linija));
                }
            }

            return returnList;
        }

        [Route("GetRedVoznje")]
        public IHttpActionResult GetRedVoznje(string dan, int linija)
        {
            List<RedVoznje> redovi = new List<RedVoznje>(db.RedoviVoznje.GetAll());
            List<string> returnList = new List<string>();
            foreach(RedVoznje red in redovi)
            {
                if(red.Dan == dan && red.LinijaId == linija)
                {
                    foreach(Voznja polazak in red.Polasci)
                    {
                        returnList.Add(polazak.Polazak);
                    }
                    return Ok(returnList);
                }
            }
            return BadRequest("Red voznje nije pronadjen");
        }

        [Route("AddVoznja")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddVoznja(VoznjaBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Voznja addVoznja = new Voznja()
            {
                LinijaId = model.IdLinije,
                Polazak = model.Polazak
            };
            try
            {
                db.Voznje.Add(addVoznja);
                db.Complete();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
            List<RedVoznje> redovi = new List<RedVoznje>(db.RedoviVoznje.GetAll());
            foreach(RedVoznje red in redovi)
            {
                if(red.LinijaId == model.IdLinije && red.Dan == model.Dan)
                {
                    try
                    {
                        red.Polasci.Add(addVoznja);
                        db.RedoviVoznje.Update(red);
                        db.Complete();
                        return Ok();
                    }
                    catch (Exception e)
                    {
                        return BadRequest();
                    }
                }
            }
            return BadRequest("Odgovarajuci red voznje nije pronadjen");
        }

        [Route("DeleteVoznja")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteVoznja(int id)
        {
            try
            {
                db.Voznje.Remove(db.Voznje.Get(id));
                db.Complete();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Route("AddStanica")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddStanica(StanicaBindingModel model)
        {
            List<Linija> linije = new List<Linija>(db.Linije.GetAll());
            List<Linija> staniceLinije = new List<Linija>();
            foreach (int id in model.Linije)
            {
                foreach(Linija lin in linije)
                {
                    if(id == lin.Id)
                    {
                        staniceLinije.Add(lin);
                        break;
                    }
                }
            }

            Stanica stanica = new Stanica()
            {
                Naziv = model.Naziv,
                Adresa = model.Adresa,
                Position = model.Position,
                Linije = staniceLinije
            };

            if(model.IdStanice != 0)
            {
                stanica.Id = model.IdStanice;
                try
                {
                    db.Stanice.Update(stanica);
                    db.Complete();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else
            {
                try
                {
                    db.Stanice.Add(stanica);
                    db.Complete();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        [Route("AddLinija")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddLinija(LinijaBindingModel model)
        {
            List<Stanica> stanice = new List<Stanica>(db.Stanice.GetAll());
            List<Stanica> linijaStanice = new List<Stanica>();
            foreach (int id in model.Stanice)
            {
                foreach (Stanica stan in stanice)
                {
                    if (id == stan.Id)
                    {
                        linijaStanice.Add(stan);
                        break;
                    }
                }
            }

            Linija linija = new Linija()
            {
                Boja = model.Boja,
                TipLinije = model.TipLinije,
                Broj = model.Broj,
                Stanice = linijaStanice
            };
            if(model.IdLinije != 0)
            {
                linija.Id = model.IdLinije;
                try
                {
                    db.Linije.Update(linija);
                    db.Complete();
                }
                catch(Exception e)
                {
                    return BadRequest();
                }
            }
            else
            {
                try
                {
                    db.Linije.Add(linija);
                    db.Complete();

                    RedVoznje radni = new RedVoznje()
                    {
                        Dan = "Radni",
                        LinijaId = linija.Id,
                        Polasci = new List<Voznja>()
                    };

                    db.RedoviVoznje.Add(radni);
                    db.Complete();

                    RedVoznje subotnji = new RedVoznje()
                    {
                        Dan = "Subota",
                        LinijaId = linija.Id,
                        Polasci = new List<Voznja>()
                    };

                    db.RedoviVoznje.Add(subotnji);
                    db.Complete();

                    RedVoznje nedeljni = new RedVoznje()
                    {
                        Dan = "Nedelja",
                        LinijaId = linija.Id,
                        Polasci = new List<Voznja>()
                    };

                    db.RedoviVoznje.Add(nedeljni);
                    db.Complete();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        [Route("DeleteLinija")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteLinija(int id)
        {
           // List<RedVoznje> redovi = new List<RedVoznje>(db.RedoviVoznje.GetAll());
            
            try
            {
                /*foreach (RedVoznje red in redovi)
                {
                    if (red.LinijaId == id)
                    {
                        red.Polasci.Clear();
                        foreach (Voznja voznja in red.Polasci)
                        {
                            db.Voznje.Remove(voznja);
                            red.Polasci.Remove(voznja);
                        }
                        db.RedoviVoznje.Remove(red);
                        redovi.Remove(red);
                    }*/
                db.Linije.Remove(db.Linije.Get(id));
                db.Complete();
            }
            catch (Exception e)
            {
                return BadRequest("Ne postoji linija sa tim id brojem");
            }
            return Ok();
        }

        [Route("DeleteStanica")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteStanica(int id)
        {
            try
            {
                db.Stanice.Remove(db.Stanice.Get(id));
                db.Complete();
            }
            catch (Exception e)
            {
                return BadRequest("Ne postoji stanica sa tim id brojem");
            }
            return Ok();
        }
    }
}
