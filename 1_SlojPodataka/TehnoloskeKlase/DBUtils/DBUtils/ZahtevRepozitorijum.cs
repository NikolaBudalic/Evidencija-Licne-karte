using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KlasePodataka;

namespace DBUtils.Repozitorijumi
{
    public class ZahtevRepozitorijum : IZahtevRepozitorijum
    {
        private readonly RVS2026LicnaKartaV1Entities db;

        public ZahtevRepozitorijum()
        {
            db = new RVS2026LicnaKartaV1Entities();
        }

        public List<Zahtev> DajSve()
        {
            return db.Zahtevs
                .Include(z => z.Gradjanin)
                .ToList();
        }

        public Zahtev DajPoId(int id)
        {
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
    }
}