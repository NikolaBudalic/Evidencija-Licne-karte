using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using KlasePodataka;

namespace DBUtils.Repozitorijumi
{
    public class ZahtevRepozitorijum : OsnovnaTehnoloskaKlasa, IZahtevRepozitorijum
    {
        private readonly RVS2026LicnaKartaV1Entities db;
        private readonly TehnoloskaObradaZahteva tehnoloskaObradaZahteva;

        public ZahtevRepozitorijum()
        {
            db = new RVS2026LicnaKartaV1Entities();
            tehnoloskaObradaZahteva = new TehnoloskaObradaZahteva();

            KreiraoKorisnik = "Sistem";
            StatusObrade = "Rad sa zahtevima preko repozitorijuma i DBUtils sloja";
        }

        public List<Zahtev> DajSve()
        {
            DataTable podaciIzProcedure = tehnoloskaObradaZahteva.DajSveZahteve();

            return db.Zahtevs
                .Include(z => z.Gradjanin)
                .ToList();
        }

        public Zahtev DajPoId(int id)
        {
            DataTable podaciIzProcedure = tehnoloskaObradaZahteva.DajZahtevPoId(id);

            return db.Zahtevs
                .Include(z => z.Gradjanin)
                .Include(z => z.RoditeljStarateljs)
                .Include(z => z.Dokumentacijas)
                .Include(z => z.IstorijaStatusaZahtevas)
                .FirstOrDefault(z => z.IDZahteva == id);
        }

        public void Dodaj(Zahtev zahtev)
        {
            db.Zahtevs.Add(zahtev);
            db.SaveChanges();
        }

        public void Izmeni(Zahtev zahtev)
        {
            db.Entry(zahtev).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Obrisi(int id)
        {
            Zahtev zahtev = db.Zahtevs
                .Include(z => z.Dokumentacijas)
                .Include(z => z.RoditeljStarateljs)
                .FirstOrDefault(z => z.IDZahteva == id);

            if (zahtev != null)
            {
                db.Dokumentacijas.RemoveRange(zahtev.Dokumentacijas);
                db.RoditeljStarateljs.RemoveRange(zahtev.RoditeljStarateljs);
                db.Zahtevs.Remove(zahtev);
                db.SaveChanges();
            }
        }

        public override string DajOpisObrade()
        {
            return "Repozitorijum za rad sa zahtevima koristi Entity Framework i DBUtils tehnološki sloj.";
        }
    }
}