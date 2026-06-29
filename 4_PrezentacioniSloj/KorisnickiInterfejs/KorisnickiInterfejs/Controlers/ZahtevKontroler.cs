using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using KlasePodataka;
using PoslovnaLogika;
using DBUtils;
using DBUtils.Repozitorijumi;
using PrezentacionaLogika.PogledModeli;
using LicnaKarta.Filteri;
using System.Data;

namespace LicnaKarta.Controllers
{
    [AutorizacijaSesijeAtribut]
    public class ZahtevController : Controller
    {
        private readonly RVS2026LicnaKartaV1Entities db;
        private readonly IZahtevRepozitorijum zahtevRepozitorijum;
        private readonly IGradjaninRepozitorijum gradjaninRepozitorijum;
        private readonly IPoslovnaPravilaServisi poslovnaPravilaService;

        public ZahtevController()
        {
            db = new RVS2026LicnaKartaV1Entities();
            zahtevRepozitorijum = new ZahtevRepozitorijum();
            gradjaninRepozitorijum = new GradjaninRepozitorijum();
            poslovnaPravilaService = new PoslovnaPravilaServisi();
        }

        public ActionResult Index(string filter)
        {
            var zahtevi = zahtevRepozitorijum.DajSve();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                TehnoloskaObradaZahteva obrada = new TehnoloskaObradaZahteva();

                // Poziv stored procedure zbog DBUtils sloja
                obrada.DajZahtevePoFilteru(filter);

                zahtevi = zahtevi
                    .Where(z =>
                        z.IDZahteva.ToString().Contains(filter) ||
                        z.JMBGGradjanina.Contains(filter) ||
                        (z.Gradjanin != null &&
                         (
                            z.Gradjanin.Ime.Contains(filter) ||
                            z.Gradjanin.Prezime.Contains(filter)
                         )) ||
                        z.StatusZahteva.Contains(filter))
                    .ToList();
            }

            ViewBag.Filter = filter;

            return View(zahtevi);
        }

        public ActionResult Create()
        {
            ZahtevPrikazModel model = new ZahtevPrikazModel
            {
                DatumPodnosenja = DateTime.Now,
                StatusZahteva = "Podnet",
                DatumRodjenja = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ZahtevPrikazModel model)
        {
            ValidirajZahtev(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool potrebanRoditelj = poslovnaPravilaService
                .DaLiSuPotrebniPodaciRoditelja(model.DatumRodjenja.Value);

            using (var transakcija = db.Database.BeginTransaction())
            {
                try
                {
                    Gradjanin gradjanin = gradjaninRepozitorijum.DajPoJMBG(model.JMBG);

                    if (gradjanin == null)
                    {
                        gradjanin = new Gradjanin
                        {
                            JMBG = model.JMBG,
                            Ime = model.Ime,
                            Prezime = model.Prezime,
                            DatumRodjenja = model.DatumRodjenja.Value,
                            Pol = model.Pol,
                            Drzavljanstvo = model.Drzavljanstvo,
                            AdresaPrebivalista = model.AdresaPrebivalista,
                            KontaktTelefon = model.KontaktTelefon,
                            Email = model.Email,
                            BrojStareLK = model.BrojStareLK,
                            DatumIstekaLK = model.DatumIstekaLK
                        };

                        db.Gradjanins.Add(gradjanin);
                        db.SaveChanges();
                    }

                    Zahtev zahtev = new Zahtev
                    {
                        JMBGGradjanina = model.JMBG,
                        DatumPodnosenja = DateTime.Now,
                        RazlogIzdavanja = model.RazlogIzdavanja,
                        TipZahteva = model.TipZahteva,
                        MestoPodnosenja = model.MestoPodnosenja,
                        StatusZahteva = "Podnet",
                        Napomena = model.Napomena
                    };

                    db.Zahtevs.Add(zahtev);
                    db.SaveChanges();

                    if (potrebanRoditelj)
                    {
                        RoditeljStaratelj roditelj = new RoditeljStaratelj
                        {
                            IDZahteva = zahtev.IDZahteva,
                            ImePrezime = model.RoditeljImePrezime,
                            JMBG = model.RoditeljJMBG,
                            Srodstvo = model.Srodstvo,
                            KontaktTelefon = model.RoditeljTelefon,
                            Email = model.RoditeljEmail
                        };

                        db.RoditeljStarateljs.Add(roditelj);
                        db.SaveChanges();
                    }

                    DodajDokumentaciju(zahtev.IDZahteva, model);

                    db.SaveChanges();
                    transakcija.Commit();

                    return RedirectToAction("Index");
                }
                catch
                {
                    transakcija.Rollback();
                    ModelState.AddModelError("", "Došlo je do greške prilikom čuvanja zahteva.");
                    return View(model);
                }
            }
        }

        public ActionResult Details(int id)
        {
            Zahtev zahtev = zahtevRepozitorijum.DajPoId(id);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            TehnoloskaObradaZahteva obrada = new TehnoloskaObradaZahteva
            {
                IDZahteva = zahtev.IDZahteva,
                TipZahteva = zahtev.TipZahteva,
                KreiraoKorisnik = Session["KorisnickoIme"] != null
                    ? Session["KorisnickoIme"].ToString()
                    : "Nepoznat korisnik"
            };

            ViewBag.OpisObrade = obrada.DajOpisObrade();
            ViewBag.KreiraoKorisnik = obrada.KreiraoKorisnik;
            ViewBag.DatumKreiranja = obrada.DatumKreiranja;

            return View(zahtev);
        }

        public ActionResult Edit(int id)
        {
            Zahtev zahtev = zahtevRepozitorijum.DajPoId(id);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            return View(zahtev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Zahtev model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Zahtev zahtev = db.Zahtevs.Find(model.IDZahteva);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            string stariStatus = zahtev.StatusZahteva;

            zahtev.RazlogIzdavanja = model.RazlogIzdavanja;
            zahtev.TipZahteva = model.TipZahteva;
            zahtev.MestoPodnosenja = model.MestoPodnosenja;
            zahtev.StatusZahteva = model.StatusZahteva;
            zahtev.Napomena = model.Napomena;

            if (stariStatus != model.StatusZahteva)
            {
                IstorijaStatusaZahteva istorija = new IstorijaStatusaZahteva
                {
                    IDZahteva = zahtev.IDZahteva,
                    StariStatus = stariStatus,
                    NoviStatus = model.StatusZahteva,
                    DatumPromene = DateTime.Now,
                    Korisnik = Session["KorisnickoIme"] != null
                        ? Session["KorisnickoIme"].ToString()
                        : "Nepoznat korisnik",
                    Napomena = "Promena statusa zahteva"
                };

                db.IstorijaStatusaZahtevas.Add(istorija);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["Uloga"] == null || Session["Uloga"].ToString() != "Administrator")
            {
                return RedirectToAction("Index");
            }

            Zahtev zahtev = zahtevRepozitorijum.DajPoId(id);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            return View(zahtev);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Uloga"] == null || Session["Uloga"].ToString() != "Administrator")
            {
                return RedirectToAction("Index");
            }

            zahtevRepozitorijum.Obrisi(id);

            return RedirectToAction("Index");
        }

        public ActionResult Stampa(int id)
        {
            TehnoloskaObradaZahteva obrada = new TehnoloskaObradaZahteva();
            DataTable podaciZaStampu = obrada.DajZahtevZaStampu(id);

            Zahtev zahtev = zahtevRepozitorijum.DajPoId(id);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            ViewBag.PodaciZaStampu = podaciZaStampu;

            return View("Stampa", zahtev);
        }

        public ActionResult ParametarskaStampa(string status)
        {
            TehnoloskaObradaZahteva obrada = new TehnoloskaObradaZahteva();
            DataTable podaciZaStampu = obrada.DajZahteveZaStampuPoStatusu(status);

            var zahtevi = db.Zahtevs
                .Include(z => z.Gradjanin)
                .Where(z => z.StatusZahteva == status)
                .ToList();

            ViewBag.Status = status;
            ViewBag.PodaciZaStampu = podaciZaStampu;

            return View("ParametarskaStampa", zahtevi);
        }

        private void ValidirajZahtev(ZahtevPrikazModel model)
        {
            if (!model.DatumRodjenja.HasValue)
            {
                ModelState.AddModelError("DatumRodjenja", "Datum rođenja je obavezan.");
                return;
            }

            if (model.DatumRodjenja.Value > DateTime.Today)
            {
                ModelState.AddModelError("DatumRodjenja", "Datum rođenja ne može biti u budućnosti.");
            }

            if (model.DatumRodjenja.Value < new DateTime(1900, 1, 1))
            {
                ModelState.AddModelError("DatumRodjenja", "Datum rođenja nije ispravan.");
            }

            bool postojiAktivanZahtev = db.Zahtevs.Any(z =>
                z.JMBGGradjanina == model.JMBG &&
                (z.StatusZahteva == "Podnet" || z.StatusZahteva == "Odobren"));

            if (postojiAktivanZahtev)
            {
                ModelState.AddModelError("", "Za ovog građanina već postoji aktivan zahtev.");
            }

            string[] dozvoljeniRazlozi =
            {
                "Prvo izdavanje",
                "Zamena",
                "Gubitak",
                "Oštećenje"
            };

            if (!dozvoljeniRazlozi.Contains(model.RazlogIzdavanja))
            {
                ModelState.AddModelError("RazlogIzdavanja",
                    "Razlog izdavanja mora biti: Prvo izdavanje, Zamena, Gubitak ili Oštećenje.");
            }

            string[] dozvoljeniTipovi =
            {
                "Redovan",
                "Hitan"
            };

            if (!dozvoljeniTipovi.Contains(model.TipZahteva))
            {
                ModelState.AddModelError("TipZahteva",
                    "Tip zahteva mora biti Redovan ili Hitan.");
            }

            bool potrebanRoditelj = poslovnaPravilaService
                .DaLiSuPotrebniPodaciRoditelja(model.DatumRodjenja.Value);

            if (potrebanRoditelj)
            {
                if (string.IsNullOrWhiteSpace(model.RoditeljImePrezime) ||
                    string.IsNullOrWhiteSpace(model.RoditeljJMBG) ||
                    string.IsNullOrWhiteSpace(model.Srodstvo) ||
                    !model.SaglasnostRoditelja)
                {
                    ModelState.AddModelError("",
                        "Za maloletno lice obavezni su podaci roditelja/staratelja i saglasnost.");
                }
            }

            if (!model.IzvodIzMaticneKnjigeRodjenih)
            {
                ModelState.AddModelError("IzvodIzMaticneKnjigeRodjenih",
                    "Izvod iz matične knjige rođenih je obavezan.");
            }

            if (!model.UverenjeODrzavljanstvu)
            {
                ModelState.AddModelError("UverenjeODrzavljanstvu",
                    "Uverenje o državljanstvu je obavezno.");
            }

            if (!model.DokazOPrebivalistu)
            {
                ModelState.AddModelError("DokazOPrebivalistu",
                    "Dokaz o prebivalištu je obavezan.");
            }

            if (!model.DokazOUplatiTakse)
            {
                ModelState.AddModelError("DokazOUplatiTakse",
                    "Dokaz o uplati takse je obavezan.");
            }
        }

        private void DodajDokumentaciju(int idZahteva, ZahtevPrikazModel model)
        {
            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Izvod iz matične knjige rođenih",
                Dostavljeno = model.IzvodIzMaticneKnjigeRodjenih
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Uverenje o državljanstvu",
                Dostavljeno = model.UverenjeODrzavljanstvu
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Dokaz o prebivalištu",
                Dostavljeno = model.DokazOPrebivalistu
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Stara lična karta",
                Dostavljeno = model.StaraLicnaKarta
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Dokaz o uplati takse",
                Dostavljeno = model.DokazOUplatiTakse
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Fotografija",
                Dostavljeno = model.Fotografija
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Saglasnost roditelja/staratelja",
                Dostavljeno = model.SaglasnostRoditelja
            });
        }
    }
}