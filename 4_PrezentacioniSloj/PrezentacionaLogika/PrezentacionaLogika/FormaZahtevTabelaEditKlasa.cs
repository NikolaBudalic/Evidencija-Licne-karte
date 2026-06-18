using System;
using System.Data;
using KlasePodataka;

namespace PrezentacionaLogika
{
    public class FormaZahtevTabelaEditKlasa
    {
        private string _stringKonekcije;

        public FormaZahtevTabelaEditKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        public DataSet DajPodatkeZaGrid(string filter)
        {
            DataSet podaciDataSet = new DataSet();

            SPZahtevDBKlasa spZahtevDBObjekat =
                new SPZahtevDBKlasa(_stringKonekcije);

            if (filter.Equals(""))
            {
                podaciDataSet = spZahtevDBObjekat.DajSveZahteve();
            }
            else
            {
                podaciDataSet = spZahtevDBObjekat.DajZahtevePoFilteru(filter);
            }

            return podaciDataSet;
        }
    }
}