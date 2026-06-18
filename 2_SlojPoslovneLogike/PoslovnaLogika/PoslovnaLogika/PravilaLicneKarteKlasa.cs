using System;
using PoslovnaLogika.Interfejsi;

namespace PoslovnaLogika
{
    public class PravilaLicneKarteKlasa : IPravilaLicneKarte
    {
        public bool DaLiJeMaloletan(DateTime datumRodjenja)
        {
            int godine = DateTime.Now.Year - datumRodjenja.Year;

            if (datumRodjenja > DateTime.Now.AddYears(-godine))
            {
                godine--;
            }

            return godine < 18;
        }

        public bool DaLiSuPotrebniPodaciRoditelja(DateTime datumRodjenja)
        {
            return DaLiJeMaloletan(datumRodjenja);
        }
    }
}