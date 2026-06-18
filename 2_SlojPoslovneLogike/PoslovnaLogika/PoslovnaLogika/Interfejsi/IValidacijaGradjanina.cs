using System;

namespace PoslovnaLogika.Interfejsi
{
    public interface IValidacijaGradjanina
    {
        bool DaLiJeJMBGIspravan(string jmbg);
        bool DaLiJeEmailIspravan(string email);

        bool DaLiSuOsnovniPodaciPopunjeni(
            string jmbg,
            string ime,
            string prezime,
            DateTime? datumRodjenja,
            string drzavljanstvo,
            string adresa);
    }
}