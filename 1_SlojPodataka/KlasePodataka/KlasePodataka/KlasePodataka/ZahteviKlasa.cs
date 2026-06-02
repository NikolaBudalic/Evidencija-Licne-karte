using System;

namespace KlasePodataka
{
    public class ZahtevKlasa
    {
        private int _idZahteva;
        private string _JMBGGradjanina;
        private DateTime _datumPodnosenja;
        private string _razlogIzdavanja;
        private string _tipZahteva;
        private string _mestoPodnosenja;
        private string _statusZahteva;
        private string _napomena;

        public int IDZahteva
        {
            get { return _idZahteva; }
            set { _idZahteva = value; }
        }

        public string JMBGGradjanina
        {
            get { return _JMBGGradjanina; }
            set { _JMBGGradjanina = value; }
        }

        public DateTime DatumPodnosenja
        {
            get { return _datumPodnosenja; }
            set { _datumPodnosenja = value; }
        }

        public string RazlogIzdavanja
        {
            get { return _razlogIzdavanja; }
            set { _razlogIzdavanja = value; }
        }

        public string TipZahteva
        {
            get { return _tipZahteva; }
            set { _tipZahteva = value; }
        }

        public string MestoPodnosenja
        {
            get { return _mestoPodnosenja; }
            set { _mestoPodnosenja = value; }
        }

        public string StatusZahteva
        {
            get { return _statusZahteva; }
            set { _statusZahteva = value; }
        }

        public string Napomena
        {
            get { return _napomena; }
            set { _napomena = value; }
        }

        public ZahtevKlasa()
        {
            _idZahteva = 0;
            _JMBGGradjanina = "";
            _razlogIzdavanja = "";
            _tipZahteva = "";
            _mestoPodnosenja = "";
            _statusZahteva = "";
            _napomena = "";
        }
    }
}