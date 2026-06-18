using System;
using System.Data;
using System.Data.SqlClient;

namespace PrezentacionaLogika
{
    public class FormaZahtevUnosKlasa
    {
        private string _stringKonekcije;

        private const int GranicaRoditelja = 18;
        private const int MinimalneGodineZaLicnuKartu = 16;

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

        public FormaZahtevUnosKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        private DateTime DajDatumRodjenjaGradjanina()
        {
            DateTime datumRodjenja = DateTime.MinValue;

            SqlConnection konekcija = new SqlConnection(_stringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajDatumRodjenjaGradjanina", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@JMBG", SqlDbType.Char).Value = JMBGGradjanina;

            object rezultat = komanda.ExecuteScalar();

            if (rezultat != null)
            {
                datumRodjenja = Convert.ToDateTime(rezultat);
            }

            konekcija.Close();
            konekcija.Dispose();

            return datumRodjenja;
        }

        private int IzracunajGodine(DateTime datumRodjenja)
        {
            int godine = DateTime.Today.Year - datumRodjenja.Year;

            if (datumRodjenja.Date > DateTime.Today.AddYears(-godine))
            {
                godine--;
            }

            return godine;
        }

        private bool DaLiGradjaninImaDovoljnoGodina()
        {
            DateTime datumRodjenja = DajDatumRodjenjaGradjanina();

            if (datumRodjenja == DateTime.MinValue)
            {
                return false;
            }

            int godine = IzracunajGodine(datumRodjenja);

            return godine >= MinimalneGodineZaLicnuKartu;
        }

        public bool DaLiJePotrebanRoditeljStaratelj()
        {
            DateTime datumRodjenja = DajDatumRodjenjaGradjanina();

            if (datumRodjenja == DateTime.MinValue)
            {
                return false;
            }

            int godine = IzracunajGodine(datumRodjenja);

            return godine < GranicaRoditelja;
        }

        private bool DaLiSuPopunjeniPodaciRoditelja()
        {
            return !string.IsNullOrWhiteSpace(RoditeljImePrezime)
                && !string.IsNullOrWhiteSpace(RoditeljJMBG)
                && !string.IsNullOrWhiteSpace(Srodstvo);
        }

        private int SnimiZahtevIVratiID()
        {
            int noviID = 0;

            SqlConnection konekcija = new SqlConnection(_stringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand(
                @"INSERT INTO Zahtev
                (JMBGGradjanina, DatumPodnosenja, RazlogIzdavanja, TipZahteva,
                 MestoPodnosenja, StatusZahteva, Napomena)
                VALUES
                (@JMBGGradjanina, @DatumPodnosenja, @RazlogIzdavanja, @TipZahteva,
                 @MestoPodnosenja, @StatusZahteva, @Napomena);
                SELECT SCOPE_IDENTITY();",
                konekcija);

            komanda.Parameters.Add("@JMBGGradjanina", SqlDbType.Char).Value = JMBGGradjanina;
            komanda.Parameters.Add("@DatumPodnosenja", SqlDbType.Date).Value = DatumPodnosenja;
            komanda.Parameters.Add("@RazlogIzdavanja", SqlDbType.NVarChar).Value = RazlogIzdavanja;
            komanda.Parameters.Add("@TipZahteva", SqlDbType.NVarChar).Value = TipZahteva;
            komanda.Parameters.Add("@MestoPodnosenja", SqlDbType.NVarChar).Value = MestoPodnosenja;
            komanda.Parameters.Add("@StatusZahteva", SqlDbType.NVarChar).Value = StatusZahteva;
            komanda.Parameters.Add("@Napomena", SqlDbType.NVarChar).Value = Napomena;

            noviID = Convert.ToInt32(komanda.ExecuteScalar());

            konekcija.Close();
            konekcija.Dispose();

            return noviID;
        }

        private bool SnimiRoditeljaStaratelja(int idZahteva)
        {
            int brojSlogova = 0;

            SqlConnection konekcija = new SqlConnection(_stringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DodajRoditeljaStaratelja", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;

            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;
            komanda.Parameters.Add("@ImePrezime", SqlDbType.NVarChar).Value = RoditeljImePrezime;
            komanda.Parameters.Add("@JMBG", SqlDbType.Char).Value = RoditeljJMBG;
            komanda.Parameters.Add("@Srodstvo", SqlDbType.NVarChar).Value = Srodstvo;
            komanda.Parameters.Add("@KontaktTelefon", SqlDbType.NVarChar).Value = RoditeljTelefon;
            komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = RoditeljEmail;

            brojSlogova = komanda.ExecuteNonQuery();

            konekcija.Close();
            konekcija.Dispose();

            return brojSlogova > 0;
        }

        private void KreirajDokumentaciju(int idZahteva, bool potrebanRoditelj)
        {
            SqlConnection konekcija = new SqlConnection(_stringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("KreirajOsnovnuDokumentacijuZaZahtev", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;

            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;
            komanda.Parameters.Add("@DaLiJeMaloletan", SqlDbType.Bit).Value = potrebanRoditelj;

            komanda.ExecuteNonQuery();

            konekcija.Close();
            konekcija.Dispose();
        }

        public bool SnimiPodatke()
        {
            bool imaDovoljnoGodina = DaLiGradjaninImaDovoljnoGodina();
            bool potrebanRoditelj = DaLiJePotrebanRoditeljStaratelj();

            if (!imaDovoljnoGodina)
            {
                StatusZahteva = "Odbijen";
                Napomena = "Zahtev je odbijen jer podnosilac nema najmanje 16 godina.";
            }
            else if (potrebanRoditelj && !DaLiSuPopunjeniPodaciRoditelja())
            {
                StatusZahteva = "Neispravan zahtev";
                Napomena = "Za maloletna lica obavezni su podaci roditelja/staratelja.";
            }
            else
            {
                StatusZahteva = "U obradi";
            }

            int idZahteva = SnimiZahtevIVratiID();

            if (idZahteva <= 0)
            {
                return false;
            }

            KreirajDokumentaciju(idZahteva, potrebanRoditelj);

            if (imaDovoljnoGodina && potrebanRoditelj && DaLiSuPopunjeniPodaciRoditelja())
            {
                return SnimiRoditeljaStaratelja(idZahteva);
            }

            return true;
        }
    }
}