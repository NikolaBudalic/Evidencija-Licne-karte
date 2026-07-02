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
        private readonly IPoslovnaPravilaServisi poslovnaPravilaServisi;

        public ZahtevController()
        {
            db = new RVS2026LicnaKartaV1Entities();
            zahtevRepozitorijum = new ZahtevRepozitorijum();
            gradjaninRepozitorijum = new GradjaninRepozitorijum();
            poslovnaPravilaServisi = new PoslovnaPravilaServisi();
        }

        public ActionResult Spisak(string pretraga)
        {
            var zahtevi = zahtevRepozitorijum.DajSve();

            if (!string.IsNullOrWhiteSpace(pretraga))
            {
                TehnoloskaObradaZahteva tehnoloskaObrada = new TehnoloskaObradaZahteva();

                // Poziv stored procedure zbog DBUtils sloja
                tehnoloskaObrada.DajZahtevePoFilteru(pretraga);

                zahtevi = zahtevi
                    .Where(z =>
                        z.IDZahteva.ToString().Contains(pretraga) ||
                        z.JMBGGradjanina.Contains(pretraga) ||
                        (z.Gradjanin != null &&
                         (
                            z.Gradjanin.Ime.Contains(pretraga) ||
                            z.Gradjanin.Prezime.Contains(pretraga)
                         )) ||
                        z.StatusZahteva.Contains(pretraga))
                    .ToList();
            }

            ViewBag.Filter = pretraga;

            return View(zahtevi);
        }

        public ActionResult Dodaj()
        {
            ZahtevPrikazModel prikazModel = new ZahtevPrikazModel
            {
                DatumPodnosenja = DateTime.Now,
                StatusZahteva = "Podnet",
                DatumRodjenja = DateTime.Today
            };

            return View(prikazModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dodaj(ZahtevPrikazModel prikazModel)
        {
            ValidirajZahtev(prikazModel);

            if (!ModelState.IsValid)
            {
                return View(prikazModel);
            }

            bool potrebanRoditelj = poslovnaPravilaServisi
                .DaLiSuPotrebniPodaciRoditelja(prikazModel.DatumRodjenja.Value);

            using (var transakcija = db.Database.BeginTransaction())
            {
                try
                {
                    Gradjanin gradjanin = gradjaninRepozitorijum.DajPoJMBG(prikazModel.JMBG);

                    if (gradjanin == null)
                    {
                        gradjanin = new Gradjanin
                        {
                            JMBG = prikazModel.JMBG,
                            Ime = prikazModel.Ime,
                            Prezime = prikazModel.Prezime,
                            DatumRodjenja = prikazModel.DatumRodjenja.Value,
                            Pol = prikazModel.Pol,
                            Drzavljanstvo = prikazModel.Drzavljanstvo,
                            AdresaPrebivalista = prikazModel.AdresaPrebivalista,
                            KontaktTelefon = prikazModel.KontaktTelefon,
                            Email = prikazModel.Email,
                            BrojStareLK = prikazModel.BrojStareLK,
                            DatumIstekaLK = prikazModel.DatumIstekaLK
                        };

                        db.Gradjanins.Add(gradjanin);
                        db.SaveChanges();
                    }

                    Zahtev zahtev = new Zahtev
                    {
                        JMBGGradjanina = prikazModel.JMBG,
                        DatumPodnosenja = DateTime.Now,
                        RazlogIzdavanja = prikazModel.RazlogIzdavanja,
                        TipZahteva = prikazModel.TipZahteva,
                        MestoPodnosenja = prikazModel.MestoPodnosenja,
                        StatusZahteva = "Podnet",
                        Napomena = prikazModel.Napomena
                    };

                    db.Zahtevs.Add(zahtev);
                    db.SaveChanges();

                    if (potrebanRoditelj)
                    {
                        RoditeljStaratelj roditelj = new RoditeljStaratelj
                        {
                            IDZahteva = zahtev.IDZahteva,
                            ImePrezime = prikazModel.RoditeljImePrezime,
                            JMBG = prikazModel.RoditeljJMBG,
                            Srodstvo = prikazModel.Srodstvo,
                            KontaktTelefon = prikazModel.RoditeljTelefon,
                            Email = prikazModel.RoditeljEmail
                        };

                        db.RoditeljStarateljs.Add(roditelj);
                        db.SaveChanges();
                    }

                    DodajDokumentaciju(zahtev.IDZahteva, prikazModel);

                    db.SaveChanges();
                    transakcija.Commit();

                    return RedirectToAction("Spisak");
                }
                catch
                {
                    transakcija.Rollback();
                    ModelState.AddModelError("", "Došlo je do greške prilikom čuvanja zahteva.");
                    return View(prikazModel);
                }
            }
        }

        public ActionResult Detalji(int id)
        {
            Zahtev zahtev = zahtevRepozitorijum.DajPoId(id);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            TehnoloskaObradaZahteva tehnoloskaObrada = new TehnoloskaObradaZahteva
            {
                IDZahteva = zahtev.IDZahteva,
                TipZahteva = zahtev.TipZahteva,
                KreiraoKorisnik = Session["KorisnickoIme"] != null
                    ? Session["KorisnickoIme"].ToString()
                    : "Nepoznat korisnik"
            };

            ViewBag.OpisObrade = tehnoloskaObrada.DajOpisObrade();
            ViewBag.KreiraoKorisnik = tehnoloskaObrada.KreiraoKorisnik;
            ViewBag.DatumKreiranja = tehnoloskaObrada.DatumKreiranja;

            return View(zahtev);
        }

        public ActionResult Izmeni(int id)
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
        public ActionResult Izmeni(Zahtev prikazModel)
        {
            if (!ModelState.IsValid)
            {
                return View(prikazModel);
            }

            Zahtev zahtev = db.Zahtevs.Find(prikazModel.IDZahteva);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            string stariStatus = zahtev.StatusZahteva;

            zahtev.RazlogIzdavanja = prikazModel.RazlogIzdavanja;
            zahtev.TipZahteva = prikazModel.TipZahteva;
            zahtev.MestoPodnosenja = prikazModel.MestoPodnosenja;
            zahtev.StatusZahteva = prikazModel.StatusZahteva;
            zahtev.Napomena = prikazModel.Napomena;

            if (stariStatus != prikazModel.StatusZahteva)
            {
                IstorijaStatusaZahteva istorija = new IstorijaStatusaZahteva
                {
                    IDZahteva = zahtev.IDZahteva,
                    StariStatus = stariStatus,
                    NoviStatus = prikazModel.StatusZahteva,
                    DatumPromene = DateTime.Now,
                    Korisnik = Session["KorisnickoIme"] != null
                        ? Session["KorisnickoIme"].ToString()
                        : "Nepoznat korisnik",
                    Napomena = "Promena statusa zahteva"
                };

                db.IstorijaStatusaZahtevas.Add(istorija);
            }

            db.SaveChanges();

            return RedirectToAction("Spisak");
        }

        public ActionResult Obrisi(int id)
        {
            if (Session["Uloga"] == null || Session["Uloga"].ToString() != "Administrator")
            {
                return RedirectToAction("Spisak");
            }

            Zahtev zahtev = zahtevRepozitorijum.DajPoId(id);

            if (zahtev == null)
            {
                return HttpNotFound();
            }

            return View(zahtev);
        }

        [HttpPost, ActionName("Obrisi")]
        [ValidateAntiForgeryToken]
        public ActionResult PotvrdiBrisanje(int id)
        {
            if (Session["Uloga"] == null || Session["Uloga"].ToString() != "Administrator")
            {
                return RedirectToAction("Spisak");
            }

            zahtevRepozitorijum.Obrisi(id);

            return RedirectToAction("Spisak");
        }

        public ActionResult Stampa(int id)
        {
            TehnoloskaObradaZahteva tehnoloskaObrada = new TehnoloskaObradaZahteva();
            DataTable podaciZaStampu = tehnoloskaObrada.DajZahtevZaStampu(id);

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
            TehnoloskaObradaZahteva tehnoloskaObrada = new TehnoloskaObradaZahteva();
            DataTable podaciZaStampu = tehnoloskaObrada.DajZahteveZaStampuPoStatusu(status);

            var zahtevi = db.Zahtevs
                .Include(z => z.Gradjanin)
                .Where(z => z.StatusZahteva == status)
                .ToList();

            ViewBag.Status = status;
            ViewBag.PodaciZaStampu = podaciZaStampu;

            return View("ParametarskaStampa", zahtevi);
        }

        private void ValidirajZahtev(ZahtevPrikazModel prikazModel)
        {
            if (!prikazModel.DatumRodjenja.HasValue)
            {
                ModelState.AddModelError("DatumRodjenja", "Datum rođenja je obavezan.");
                return;
            }

            if (prikazModel.DatumRodjenja.Value > DateTime.Today)
            {
                ModelState.AddModelError("DatumRodjenja", "Datum rođenja ne može biti u budućnosti.");
            }

            if (prikazModel.DatumRodjenja.Value < new DateTime(1900, 1, 1))
            {
                ModelState.AddModelError("DatumRodjenja", "Datum rođenja nije ispravan.");
            }

            bool postojiAktivanZahtev = db.Zahtevs.Any(z =>
                z.JMBGGradjanina == prikazModel.JMBG &&
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

            if (!dozvoljeniRazlozi.Contains(prikazModel.RazlogIzdavanja))
            {
                ModelState.AddModelError("RazlogIzdavanja",
                    "Razlog izdavanja mora biti: Prvo izdavanje, Zamena, Gubitak ili Oštećenje.");
            }

            string[] dozvoljeniTipovi =
            {
                "Redovan",
                "Hitan"
            };

            if (!dozvoljeniTipovi.Contains(prikazModel.TipZahteva))
            {
                ModelState.AddModelError("TipZahteva",
                    "Tip zahteva mora biti Redovan ili Hitan.");
            }

            bool potrebanRoditelj = poslovnaPravilaServisi
                .DaLiSuPotrebniPodaciRoditelja(prikazModel.DatumRodjenja.Value);

            if (potrebanRoditelj)
            {
                if (string.IsNullOrWhiteSpace(prikazModel.RoditeljImePrezime) ||
                    string.IsNullOrWhiteSpace(prikazModel.RoditeljJMBG) ||
                    string.IsNullOrWhiteSpace(prikazModel.Srodstvo) ||
                    !prikazModel.SaglasnostRoditelja)
                {
                    ModelState.AddModelError("",
                        "Za maloletno lice obavezni su podaci roditelja/staratelja i saglasnost.");
                }
            }

            if (!prikazModel.IzvodIzMaticneKnjigeRodjenih)
            {
                ModelState.AddModelError("IzvodIzMaticneKnjigeRodjenih",
                    "Izvod iz matične knjige rođenih je obavezan.");
            }

            if (!prikazModel.UverenjeODrzavljanstvu)
            {
                ModelState.AddModelError("UverenjeODrzavljanstvu",
                    "Uverenje o državljanstvu je obavezno.");
            }

            if (!prikazModel.DokazOPrebivalistu)
            {
                ModelState.AddModelError("DokazOPrebivalistu",
                    "Dokaz o prebivalištu je obavezan.");
            }

            if (!prikazModel.DokazOUplatiTakse)
            {
                ModelState.AddModelError("DokazOUplatiTakse",
                    "Dokaz o uplati takse je obavezan.");
            }
        }

        private void DodajDokumentaciju(int idZahteva, ZahtevPrikazModel prikazModel)
        {
            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Izvod iz matične knjige rođenih",
                Dostavljeno = prikazModel.IzvodIzMaticneKnjigeRodjenih
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Uverenje o državljanstvu",
                Dostavljeno = prikazModel.UverenjeODrzavljanstvu
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Dokaz o prebivalištu",
                Dostavljeno = prikazModel.DokazOPrebivalistu
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Stara lična karta",
                Dostavljeno = prikazModel.StaraLicnaKarta
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Dokaz o uplati takse",
                Dostavljeno = prikazModel.DokazOUplatiTakse
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Fotografija",
                Dostavljeno = prikazModel.Fotografija
            });

            db.Dokumentacijas.Add(new Dokumentacija
            {
                IDZahteva = idZahteva,
                NazivDokumenta = "Saglasnost roditelja/staratelja",
                Dostavljeno = prikazModel.SaglasnostRoditelja
            });
        }
    }
}