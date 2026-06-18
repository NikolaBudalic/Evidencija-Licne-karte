using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace PoslovnaLogika
{
    public class PoslovnaPravilaServisi : IPoslovnaPravilaServisi
    {
        public int DajStarosnuGranicu()
        {
            string putanja = HttpContext.Current.Server.MapPath("~/Data/poslovna_pravila.json");

            string json = File.ReadAllText(putanja);

            dynamic podaci = JsonConvert.DeserializeObject(json);

            return (int)podaci.StarosnaGranicaZaSaglasnost;
        }

        public bool DaLiJeMaloletan(DateTime datumRodjenja)
        {
            int starosnaGranica = DajStarosnuGranicu();

            int godine = DateTime.Now.Year - datumRodjenja.Year;

            if (datumRodjenja > DateTime.Now.AddYears(-godine))
            {
                godine--;
            }

            return godine < starosnaGranica;
        }

        public bool DaLiSuPotrebniPodaciRoditelja(DateTime datumRodjenja)
        {
            return DaLiJeMaloletan(datumRodjenja);
        }
    }
}