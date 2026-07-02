using System.Linq;
using System.Web.Mvc;
using DBUtils;
using DBUtils.Repozitorijumi;
using KlasePodataka;
using LicnaKarta.Filteri;

namespace LicnaKarta.Controllers
{
    [AutorizacijaSesijeAtribut]
    public class GradjaninController : Controller
    {
        private readonly IGradjaninRepozitorijum gradjaninRepozitorijum;
        private readonly TehnoloskaObradaGradjana tehnoloskaObradaGradjana;

        public GradjaninController()
        {
            gradjaninRepozitorijum = new GradjaninRepozitorijum();
            tehnoloskaObradaGradjana = new TehnoloskaObradaGradjana();
        }

        public ActionResult Spisak(string pretraga)
        {
            var gradjani = gradjaninRepozitorijum.DajSve();

            if (!string.IsNullOrWhiteSpace(pretraga))
            {
                tehnoloskaObradaGradjana.DajGradjanePoFilteru(pretraga);

                gradjani = gradjani
                    .Where(g =>
                        g.JMBG.Contains(pretraga) ||
                        g.Ime.Contains(pretraga) ||
                        g.Prezime.Contains(pretraga))
                    .ToList();
            }

            ViewBag.Filter = pretraga;

            return View(gradjani);
        }

        public ActionResult Detalji(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            Gradjanin gradjanin = gradjaninRepozitorijum.DajPoJMBG(id);

            if (gradjanin == null)
            {
                return HttpNotFound();
            }

            return View(gradjanin);
        }

        public ActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dodaj(Gradjanin gradjanin)
        {
            ValidirajGradjanina(gradjanin, true);

            if (!ModelState.IsValid)
            {
                return View(gradjanin);
            }

            gradjaninRepozitorijum.Dodaj(gradjanin);

            return RedirectToAction("Spisak");
        }

        public ActionResult Izmeni(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            Gradjanin gradjanin = gradjaninRepozitorijum.DajPoJMBG(id);

            if (gradjanin == null)
            {
                return HttpNotFound();
            }

            return View(gradjanin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Izmeni(Gradjanin gradjanin)
        {
            ValidirajGradjanina(gradjanin, false);

            if (!ModelState.IsValid)
            {
                return View(gradjanin);
            }

            gradjaninRepozitorijum.Izmeni(gradjanin);

            return RedirectToAction("Spisak");
        }

        public ActionResult Obrisi(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            Gradjanin gradjanin = gradjaninRepozitorijum.DajPoJMBG(id);

            if (gradjanin == null)
            {
                return HttpNotFound();
            }

            return View(gradjanin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PotvrdiBrisanje(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            gradjaninRepozitorijum.Obrisi(id);

            return RedirectToAction("Spisak");
        }

        private void ValidirajGradjanina(Gradjanin gradjanin, bool proveriDuplikat)
        {
            if (gradjanin == null)
            {
                ModelState.AddModelError("", "Podaci o građaninu nisu ispravno poslati.");
                return;
            }

            if (string.IsNullOrWhiteSpace(gradjanin.JMBG) || gradjanin.JMBG.Length != 13)
            {
                ModelState.AddModelError("JMBG", "JMBG mora imati tačno 13 cifara.");
            }

            if (string.IsNullOrWhiteSpace(gradjanin.Ime))
            {
                ModelState.AddModelError("Ime", "Ime je obavezno.");
            }

            if (string.IsNullOrWhiteSpace(gradjanin.Prezime))
            {
                ModelState.AddModelError("Prezime", "Prezime je obavezno.");
            }

            if (string.IsNullOrWhiteSpace(gradjanin.Drzavljanstvo))
            {
                ModelState.AddModelError("Drzavljanstvo", "Državljanstvo je obavezno.");
            }

            if (string.IsNullOrWhiteSpace(gradjanin.AdresaPrebivalista))
            {
                ModelState.AddModelError("AdresaPrebivalista", "Adresa prebivališta je obavezna.");
            }

            if (proveriDuplikat && gradjaninRepozitorijum.DajPoJMBG(gradjanin.JMBG) != null)
            {
                ModelState.AddModelError("JMBG", "Građanin sa unetim JMBG-om već postoji.");
            }
        }
    }
}