using System.Web.Mvc;
using DBUtils.Repozitorijumi;
using KlasePodataka;
using LicnaKarta.Filteri;

namespace LicnaKarta.Controllers
{
    [AutorizacijaSesijeAtribut]
    public class GradjaninController : Controller
    {
        private readonly IGradjaninRepozitorijum gradjaninRepozitorijum;

        public GradjaninController()
        {
            gradjaninRepozitorijum = new GradjaninRepozitorijum();
        }

        public ActionResult Index()
        {
            var gradjani = gradjaninRepozitorijum.DajSve();
            return View(gradjani);
        }

        public ActionResult Details(string id)
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gradjanin gradjanin)
        {
            ValidirajGradjanina(gradjanin, true);

            if (!ModelState.IsValid)
            {
                return View(gradjanin);
            }

            gradjaninRepozitorijum.Dodaj(gradjanin);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
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
        public ActionResult Edit(Gradjanin gradjanin)
        {
            ValidirajGradjanina(gradjanin, false);

            if (!ModelState.IsValid)
            {
                return View(gradjanin);
            }

            gradjaninRepozitorijum.Izmeni(gradjanin);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            gradjaninRepozitorijum.Obrisi(id);

            return RedirectToAction("Index");
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