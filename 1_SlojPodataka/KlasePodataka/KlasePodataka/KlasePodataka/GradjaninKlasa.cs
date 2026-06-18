using System;

namespace KlasePodataka
{
    public class GradjaninKlasa
    {
        private string _JMBG;
        private string _ime;
        private string _prezime;
        private DateTime _datumRodjenja;
        private string _pol;
        private string _drzavljanstvo;
        private string _adresaPrebivalista;
        private string _kontaktTelefon;
        private string _email;
        private string _brojStareLK;

        public string JMBG
        {
            get { return _JMBG; }
            set { _JMBG = value; }
        }

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        public DateTime DatumRodjenja
        {
            get { return _datumRodjenja; }
            set { _datumRodjenja = value; }
        }

        public string Pol
        {
            get { return _pol; }
            set { _pol = value; }
        }

        public string Drzavljanstvo
        {
            get { return _drzavljanstvo; }
            set { _drzavljanstvo = value; }
        }

        public string AdresaPrebivalista
        {
            get { return _adresaPrebivalista; }
            set { _adresaPrebivalista = value; }
        }

        public string KontaktTelefon
        {
            get { return _kontaktTelefon; }
            set { _kontaktTelefon = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string BrojStareLK
        {
            get { return _brojStareLK; }
            set { _brojStareLK = value; }
        }
    }
}