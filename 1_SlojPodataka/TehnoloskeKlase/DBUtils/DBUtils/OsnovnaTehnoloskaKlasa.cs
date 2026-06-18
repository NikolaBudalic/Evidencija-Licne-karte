using System;

namespace DBUtils
{
    public abstract class OsnovnaTehnoloskaKlasa
    {
        public DateTime DatumKreiranja { get; set; }

        public string KreiraoKorisnik { get; set; }

        public string StatusObrade { get; set; }

        public OsnovnaTehnoloskaKlasa()
        {
            DatumKreiranja = DateTime.Now;
            StatusObrade = "Aktivan";
        }

        public virtual string DajOpisObrade()
        {
            return "Osnovna tehnološka obrada podataka u sistemu.";
        }
    }
}