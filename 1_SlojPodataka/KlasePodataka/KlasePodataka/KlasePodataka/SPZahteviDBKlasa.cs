using System;
using System.Data;
using System.Data.SqlClient;

namespace KlasePodataka
{
    public class SPZahtevDBKlasa
    {
        private string _stringKonekcije;

        public string StringKonekcije
        {
            get { return _stringKonekcije; }
        }

        public SPZahtevDBKlasa(string noviStringKonekcije)
        {
            _stringKonekcije = noviStringKonekcije;
        }

        public DataSet DajSveZahteve()
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
                "DajSveZahteve",
                pomKonekcija);

            pomKomanda.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;
            adapter.Fill(podaciDataSet);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return podaciDataSet;
        }

        public DataSet DajZahtevePoStatusu(string statusFilter)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
                "DajZahtevePoStatusu",
                pomKonekcija);

            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add(
                "@StatusZahteva",
                SqlDbType.NVarChar).Value = statusFilter;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;
            adapter.Fill(podaciDataSet);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return podaciDataSet;
        }

        public DataSet DajZahtevePoFilteru(string filter)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
                "DajZahtevePoFilteru",
                pomKonekcija);

            pomKomanda.CommandType = CommandType.StoredProcedure;

            pomKomanda.Parameters.Add(
                "@Filter",
                SqlDbType.NVarChar).Value = filter;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = pomKomanda;
            adapter.Fill(podaciDataSet);

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return podaciDataSet;
        }

        public bool ObrisiZahtev(int idZahteva)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
                "DELETE FROM Zahtev WHERE IDZahteva=@IDZahteva",
                pomKonekcija);

            pomKomanda.Parameters.Add(
                "@IDZahteva",
                SqlDbType.Int).Value = idZahteva;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return (brojSlogova > 0);
        }

        public bool SnimiNoviZahtev(ZahtevKlasa noviZahtevObjekat)
        {
            int brojSlogova = 0;

            SqlConnection pomKonekcija = new SqlConnection(_stringKonekcije);
            pomKonekcija.Open();

            SqlCommand pomKomanda = new SqlCommand(
            @"INSERT INTO Zahtev
            (JMBGGradjanina,
            DatumPodnosenja,
            RazlogIzdavanja,
            TipZahteva,
            MestoPodnosenja,
            StatusZahteva,
            Napomena)

            VALUES
            (@JMBGGradjanina,
            @DatumPodnosenja,
            @RazlogIzdavanja,
            @TipZahteva,
            @MestoPodnosenja,
            @StatusZahteva,
            @Napomena)",
            pomKonekcija);

            pomKomanda.Parameters.Add("@JMBGGradjanina", SqlDbType.NVarChar).Value =
                noviZahtevObjekat.JMBGGradjanina;

            pomKomanda.Parameters.Add("@DatumPodnosenja", SqlDbType.Date).Value =
                noviZahtevObjekat.DatumPodnosenja;

            pomKomanda.Parameters.Add("@RazlogIzdavanja", SqlDbType.NVarChar).Value =
                noviZahtevObjekat.RazlogIzdavanja;

            pomKomanda.Parameters.Add("@TipZahteva", SqlDbType.NVarChar).Value =
                noviZahtevObjekat.TipZahteva;

            pomKomanda.Parameters.Add("@MestoPodnosenja", SqlDbType.NVarChar).Value =
                noviZahtevObjekat.MestoPodnosenja;

            pomKomanda.Parameters.Add("@StatusZahteva", SqlDbType.NVarChar).Value =
                noviZahtevObjekat.StatusZahteva;

            pomKomanda.Parameters.Add("@Napomena", SqlDbType.NVarChar).Value =
                noviZahtevObjekat.Napomena;

            brojSlogova = pomKomanda.ExecuteNonQuery();

            pomKonekcija.Close();
            pomKonekcija.Dispose();

            return (brojSlogova > 0);
        }
    }
}