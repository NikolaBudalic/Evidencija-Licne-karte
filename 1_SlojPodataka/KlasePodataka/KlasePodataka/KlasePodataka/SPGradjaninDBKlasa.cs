using System;
using System.Data;
using System.Data.SqlClient;

namespace KlasePodataka
{
    public class SPGradjaninDBKlasa
    {
        private string _stringKonekcije;

        public string StringKonekcije
        {
            get { return _stringKonekcije; }
        }

        public SPGradjaninDBKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        public DataSet DajSveGradjane()
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
                "SELECT * FROM Gradjanin",
                pomKonekcija);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;
            adapter.Fill(podaciDataSet);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return podaciDataSet;
        }

        public DataSet DajGradjaninaPoJMBG(string jmbgFilter)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
                "SELECT * FROM Gradjanin WHERE JMBG=@JMBG",
                pomKonekcija);

            pomKomanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = jmbgFilter;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;
            adapter.Fill(podaciDataSet);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return podaciDataSet;
        }

        public bool SnimiNovogGradjanina(GradjaninKlasa noviGradjaninObjekat)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
            @"INSERT INTO Gradjanin
            (JMBG, Ime, Prezime, DatumRodjenja, Pol,
            Drzavljanstvo, AdresaPrebivalista,
            KontaktTelefon, Email, BrojStareLK)

            VALUES
            (@JMBG, @Ime, @Prezime, @DatumRodjenja, @Pol,
            @Drzavljanstvo, @AdresaPrebivalista,
            @KontaktTelefon, @Email, @BrojStareLK)",
            pomKonekcija);

            pomKomanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = noviGradjaninObjekat.JMBG;
            pomKomanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = noviGradjaninObjekat.Ime;
            pomKomanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = noviGradjaninObjekat.Prezime;
            pomKomanda.Parameters.Add("@DatumRodjenja", SqlDbType.Date).Value = noviGradjaninObjekat.DatumRodjenja;
            pomKomanda.Parameters.Add("@Pol", SqlDbType.NVarChar).Value = noviGradjaninObjekat.Pol;
            pomKomanda.Parameters.Add("@Drzavljanstvo", SqlDbType.NVarChar).Value = noviGradjaninObjekat.Drzavljanstvo;
            pomKomanda.Parameters.Add("@AdresaPrebivalista", SqlDbType.NVarChar).Value = noviGradjaninObjekat.AdresaPrebivalista;
            pomKomanda.Parameters.Add("@KontaktTelefon", SqlDbType.NVarChar).Value = noviGradjaninObjekat.KontaktTelefon;
            pomKomanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = noviGradjaninObjekat.Email;
            pomKomanda.Parameters.Add("@BrojStareLK", SqlDbType.NVarChar).Value = noviGradjaninObjekat.BrojStareLK;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return (brojSlogova > 0);
        }

        public bool ObrisiGradjanina(string jmbgGradjanina)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
                "DELETE FROM Gradjanin WHERE JMBG=@JMBG",
                pomKonekcija);

            pomKomanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = jmbgGradjanina;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return (brojSlogova > 0);
        }
    }
}