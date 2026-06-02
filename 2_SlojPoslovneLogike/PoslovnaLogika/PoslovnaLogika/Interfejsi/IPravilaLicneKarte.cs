using System;

namespace PoslovnaLogika.Interfejsi
{
    public interface IPravilaLicneKarte
    {
        bool DaLiJeMaloletan(DateTime datumRodjenja);
        bool DaLiSuPotrebniPodaciRoditelja(DateTime datumRodjenja);
    }
}