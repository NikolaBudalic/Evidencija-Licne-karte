using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using KlasePodataka;

namespace LicnaKarta.Controllers.Api
{
    [RoutePrefix("api/zahtevi")]
    public class ZahteviApiController : ApiController
    {
        private readonly RVS2026LicnaKartaV1Entities db = new RVS2026LicnaKartaV1Entities();

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var zahtevi = db.Zahtevs
                .Include(z => z.Gradjanin)
                .Select(z => new
                {
                    z.IDZahteva,
                    z.JMBGGradjanina,
                    Ime = z.Gradjanin.Ime,
                    Prezime = z.Gradjanin.Prezime,
                    z.DatumPodnosenja,
                    z.RazlogIzdavanja,
                    z.TipZahteva,
                    z.MestoPodnosenja,
                    z.StatusZahteva
                })
                .ToList();

            return Ok(zahtevi);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var zahtev = db.Zahtevs
                .Include(z => z.Gradjanin)
                .Where(z => z.IDZahteva == id)
                .Select(z => new
                {
                    z.IDZahteva,
                    z.JMBGGradjanina,
                    Ime = z.Gradjanin.Ime,
                    Prezime = z.Gradjanin.Prezime,
                    z.DatumPodnosenja,
                    z.RazlogIzdavanja,
                    z.TipZahteva,
                    z.MestoPodnosenja,
                    z.StatusZahteva,
                    z.Napomena
                })
                .FirstOrDefault();

            if (zahtev == null)
            {
                return NotFound();
            }

            return Ok(zahtev);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(Zahtev zahtev)
        {
            if (zahtev == null)
            {
                return BadRequest("Podaci nisu poslati.");
            }

            db.Zahtevs.Add(zahtev);
            db.SaveChanges();

            return Ok("Zahtev je uspešno dodat.");
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Zahtev zahtev)
        {
            Zahtev postojeci = db.Zahtevs.Find(id);

            if (postojeci == null)
            {
                return NotFound();
            }

            postojeci.RazlogIzdavanja = zahtev.RazlogIzdavanja;
            postojeci.TipZahteva = zahtev.TipZahteva;
            postojeci.MestoPodnosenja = zahtev.MestoPodnosenja;
            postojeci.StatusZahteva = zahtev.StatusZahteva;
            postojeci.Napomena = zahtev.Napomena;

            db.SaveChanges();

            return Ok("Zahtev je uspešno izmenjen.");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Zahtev zahtev = db.Zahtevs.Find(id);

            if (zahtev == null)
            {
                return NotFound();
            }

            db.Zahtevs.Remove(zahtev);
            db.SaveChanges();

            return Ok("Zahtev je uspešno obrisan.");
        }
    }
}