using System;
using PoslovnaLogika.Interfejsi;

namespace PoslovnaLogika
{
    public class ObradaZahtevaKlasa
    {
        private IPravilaLicneKarte pravilaObjekat;
        private IValidacijaGradjanina validacijaObjekat;

        public ObradaZahtevaKlasa()
        {
            pravilaObjekat = new PravilaLicneKarteKlasa();
            validacijaObjekat = new ValidacijaGradjaninaKlasa();
        }

        public ObradaZahtevaKlasa(
            IPravilaLicneKarte novaPravila,
            IValidacijaGradjanina novaValidacija)
        {
            pravilaObjekat = novaPravila;
            validacijaObjekat = novaValidacija;
        }

        public bool DaLiZahtevMozeDaSeSnimi(
            string jmbg,
            string ime,
            string prezime,
            DateTime datumRodjenja,
            string drzavljanstvo,
            string adresa,
            string email,
            string imePrezimeRoditelja,
            string jmbgRoditelja,
            string srodstvo)
        {
            bool osnovniPodaciIspravni =
                validacijaObjekat.DaLiSuOsnovniPodaciPopunjeni(
                    jmbg,
                    ime,
                    prezime,
                    datumRodjenja,
                    drzavljanstvo,
                    adresa);

            if (!osnovniPodaciIspravni)
            {
                return false;
            }

            if (!validacijaObjekat.DaLiJeEmailIspravan(email))
            {
                return false;
            }

            if (pravilaObjekat.DaLiJeMaloletan(datumRodjenja))
            {
                if (string.IsNullOrWhiteSpace(imePrezimeRoditelja) ||
                    string.IsNullOrWhiteSpace(jmbgRoditelja) ||
                    string.IsNullOrWhiteSpace(srodstvo))
                {
                    return false;
                }
            }

            return true;
        }

        public string OdrediStatusNaOsnovuDokumentacije(bool svaDokumentaDostavljena)
        {
            if (svaDokumentaDostavljena)
            {
                return "Odobren";
            }

            return "Na proveri";
        }

        public DateTime IzracunajDatumIstekaNoveLicneKarte(DateTime datumIzdavanja)
        {
            return datumIzdavanja.AddYears(10);
        }
    }
}