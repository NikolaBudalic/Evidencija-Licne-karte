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

            if (razlogIzdavanja == "Zamena zbog isteka")
            {
                return "ZAMENA_ISTEK";
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
            if (statusZahteva == "U obradi")
            {
                return "U_OBRADI";
            }

            if (statusZahteva == "Na proveri")
            {
                return "NA_PROVERI";
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
    }
}