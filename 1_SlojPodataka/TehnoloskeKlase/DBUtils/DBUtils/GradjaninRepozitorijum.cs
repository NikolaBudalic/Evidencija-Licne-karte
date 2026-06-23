using System.Collections.Generic;
using System.Linq;
using KlasePodataka;

namespace DBUtils.Repozitorijumi
{
    public class GradjaninRepozitorijum : OsnovnaTehnoloskaKlasa, IGradjaninRepozitorijum
    {
        private readonly RVS2026LicnaKartaV1Entities db;

        public GradjaninRepozitorijum()
        {
            db = new RVS2026LicnaKartaV1Entities();

            KreiraoKorisnik = "Sistem";
            StatusObrade = "Rad sa zahtevima";
        }

        public List<Gradjanin> DajSve()
        {
            return db.Gradjanins.ToList();
        }

        public Gradjanin DajPoJMBG(string jmbg)
        {
            return db.Gradjanins.FirstOrDefault(g => g.JMBG == jmbg);
        }

        public void Dodaj(Gradjanin gradjanin)
        {
            db.Gradjanins.Add(gradjanin);
            db.SaveChanges();
        }

        public void Izmeni(Gradjanin gradjanin)
        {
            Gradjanin postojeci = db.Gradjanins
                .FirstOrDefault(g => g.JMBG == gradjanin.JMBG);

            if (postojeci != null)
            {
                postojeci.Ime = gradjanin.Ime;
                postojeci.Prezime = gradjanin.Prezime;
                postojeci.DatumRodjenja = gradjanin.DatumRodjenja;
                postojeci.Pol = gradjanin.Pol;
                postojeci.Drzavljanstvo = gradjanin.Drzavljanstvo;
                postojeci.AdresaPrebivalista = gradjanin.AdresaPrebivalista;
                postojeci.KontaktTelefon = gradjanin.KontaktTelefon;
                postojeci.Email = gradjanin.Email;
                postojeci.BrojStareLK = gradjanin.BrojStareLK;
                postojeci.DatumIstekaLK = gradjanin.DatumIstekaLK;

                db.SaveChanges();
            }
        }

        public void Obrisi(string jmbg)
        {
            Gradjanin gradjanin = db.Gradjanins
                .FirstOrDefault(g => g.JMBG == jmbg);

            if (gradjanin != null)
            {
                db.Gradjanins.Remove(gradjanin);
                db.SaveChanges();
            }
        }
    }
}