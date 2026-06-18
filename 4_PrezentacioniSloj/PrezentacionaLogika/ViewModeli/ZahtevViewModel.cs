using System;

namespace PrezentacionaLogika.ViewModeli
{
    public class ZahtevViewModel
    {
        public string JMBGGradjanina { get; set; }
        public DateTime DatumPodnosenja { get; set; }
        public string RazlogIzdavanja { get; set; }
        public string TipZahteva { get; set; }
        public string MestoPodnosenja { get; set; }
        public string StatusZahteva { get; set; }
        public string Napomena { get; set; }

        public string RoditeljImePrezime { get; set; }
        public string RoditeljJMBG { get; set; }
        public string Srodstvo { get; set; }
        public string RoditeljTelefon { get; set; }
        public string RoditeljEmail { get; set; }
    }
}