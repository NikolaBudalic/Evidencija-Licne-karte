using System;

namespace KlaseMapiranja
{
    public class MapiranjeLicneKarteKlasa
    {
        public string DajTipZahtevaZaServis(string razlogIzdavanja)
        {
            if (razlogIzdavanja == "Prvo izdavanje")
            {
                return "PRVO_IZDAVANJE";
            }

            if (razlogIzdavanja == "Zamena")
            {
                return "ZAMENA";
            }

            if (razlogIzdavanja == "Gubitak")
            {
                return "GUBITAK";
            }

            if (razlogIzdavanja == "Oštećenje")
            {
                return "OSTECENJE";
            }

            return "NEPOZNATO";
        }

        public string DajStatusZaServis(string statusZahteva)
        {
            if (statusZahteva == "Podnet")
            {
                return "PODNET";
            }

            if (statusZahteva == "U obradi")
            {
                return "U_OBRADI";
            }

            if (statusZahteva == "Odobren")
            {
                return "ODOBREN";
            }

            if (statusZahteva == "Odbijen")
            {
                return "ODBIJEN";
            }

            return "NEPOZNAT_STATUS";
        }

        public string DajOpisZahtevaZaServis(
            string ime,
            string prezime,
            string razlogIzdavanja,
            string statusZahteva)
        {
            return ime + " " + prezime + " - " +
                   DajTipZahtevaZaServis(razlogIzdavanja) + " - " +
                   DajStatusZaServis(statusZahteva);
        }
    }
}