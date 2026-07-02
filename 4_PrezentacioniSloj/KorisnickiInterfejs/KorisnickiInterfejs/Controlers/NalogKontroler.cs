using System.Linq;
using System.Web.Mvc;
using KlasePodataka;
using PrezentacionaLogika.PogledModeli;

namespace LicnaKarta.Controllers
{
    public class NalogController : Controller
    {
        private readonly RVS2026LicnaKartaV1Entities db =
            new RVS2026LicnaKartaV1Entities();

        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prijava(PrijavaPrikazModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Korisnik korisnik = db.Korisniks.FirstOrDefault(k =>
                k.KorisnickoIme == model.KorisnickoIme &&
                k.Sifra == model.Sifra);

            if (korisnik == null)
            {
                ModelState.AddModelError("", "Pogrešno korisničko ime ili lozinka.");
                return View(model);
            }

            Session["IDKorisnika"] = korisnik.IDKorisnika;
            Session["KorisnickoIme"] = korisnik.KorisnickoIme;
            Session["Ime"] = korisnik.Ime;
            Session["Prezime"] = korisnik.Prezime;
            Session["Uloga"] = korisnik.Uloga;

            return RedirectToAction("Spisak", "Zahtev");
        }

        public ActionResult Odjava()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Prijava");
        }
    }
}