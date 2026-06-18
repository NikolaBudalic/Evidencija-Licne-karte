using System;
using System.Text.RegularExpressions;
using PoslovnaLogika.Interfejsi;

namespace PoslovnaLogika
{
    public class ValidacijaGradjaninaKlasa : IValidacijaGradjanina
    {
        public bool DaLiJeJMBGIspravan(string jmbg)
        {
            return !string.IsNullOrWhiteSpace(jmbg)
                && Regex.IsMatch(jmbg, @"^\d{13}$");
        }

        public bool DaLiJeEmailIspravan(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return true;
            }

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool DaLiSuOsnovniPodaciPopunjeni(
            string jmbg,
            string ime,
            string prezime,
            DateTime? datumRodjenja,
            string drzavljanstvo,
            string adresa)
        {
            return DaLiJeJMBGIspravan(jmbg)
                && !string.IsNullOrWhiteSpace(ime)
                && !string.IsNullOrWhiteSpace(prezime)
                && datumRodjenja.HasValue
                && !string.IsNullOrWhiteSpace(drzavljanstvo)
                && !string.IsNullOrWhiteSpace(adresa);
        }
    }
}