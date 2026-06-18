using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace KorisnickiInterfejs
{
    public partial class ZahteviStampa : System.Web.UI.Page
    {
        private string StringKonekcije
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString;
            }
        }

        private void PrikaziZahtevZaStampu(int idZahteva)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajZahtevZaStampu", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komanda;
            adapter.Fill(podaciDataSet);

            konekcija.Close();
            konekcija.Dispose();

            if (podaciDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow red = podaciDataSet.Tables[0].Rows[0];

                JMBGLabel.Text = red["JMBG"].ToString();
                ImePrezimeLabel.Text = red["Ime"].ToString();
                PrezimeLabel.Text = red["Prezime"].ToString();

                DatumRodjenjaLabel.Text =
                    Convert.ToDateTime(red["DatumRodjenja"]).ToString("dd.MM.yyyy");

                PolLabel.Text = red["Pol"].ToString();
                DrzavljanstvoLabel.Text = red["Drzavljanstvo"].ToString();
                AdresaLabel.Text = red["AdresaPrebivalista"].ToString();
                TelefonLabel.Text = red["KontaktTelefon"].ToString();
                EmailLabel.Text = red["Email"].ToString();
                if (red["BrojStareLK"] != DBNull.Value && red["BrojStareLK"].ToString() != "")
                {
                    BrojStareLKLabel.Text = red["BrojStareLK"].ToString();
                }
                else
                {
                    BrojStareLKLabel.Text = "/";
                }

                if (red["DatumIstekaLK"] != DBNull.Value)
                {
                    DatumIstekaLKLabel.Text =
                        Convert.ToDateTime(red["DatumIstekaLK"]).ToString("dd.MM.yyyy");
                }
                else
                {
                    DatumIstekaLKLabel.Text = "/";
                }
                BrojNoveLKLabel.Text =
                    red["BrojNoveLK"] != DBNull.Value && red["BrojNoveLK"].ToString() != ""
                  ? red["BrojNoveLK"].ToString()
                    : "/";

                if (red["DatumIstekaNoveLK"] != DBNull.Value)
                {
                    DatumIstekaNoveLKLabel.Text =
                        Convert.ToDateTime(red["DatumIstekaNoveLK"]).ToString("dd.MM.yyyy");
                }
                else
                {
                    DatumIstekaNoveLKLabel.Text = "/";
                }

                DatumPodnosenjaLabel.Text =
                    Convert.ToDateTime(red["DatumPodnosenja"]).ToString("dd.MM.yyyy");

                RazlogLabel.Text = red["RazlogIzdavanja"].ToString();
                TipLabel.Text = red["TipZahteva"].ToString();
                MestoLabel.Text = red["MestoPodnosenja"].ToString();
                StatusZahtevaLabel.Text = red["StatusZahteva"].ToString();
                NapomenaLabel.Text = red["Napomena"].ToString();
            }
        }

        private void PrikaziRoditeljaStaratelja(int idZahteva)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajRoditeljaZaZahtev", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komanda;
            adapter.Fill(podaciDataSet);

            konekcija.Close();
            konekcija.Dispose();

            if (podaciDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow red = podaciDataSet.Tables[0].Rows[0];

                RoditeljImePrezimeLabel.Text = red["ImePrezime"].ToString();
                RoditeljJMBGLabel.Text = red["JMBG"].ToString();
                SrodstvoLabel.Text = red["Srodstvo"].ToString();
                RoditeljTelefonLabel.Text = red["KontaktTelefon"].ToString();
                RoditeljEmailLabel.Text = red["Email"].ToString();
            }
            else
            {
                RoditeljImePrezimeLabel.Text = "/";
                RoditeljJMBGLabel.Text = "/";
                SrodstvoLabel.Text = "/";
                RoditeljTelefonLabel.Text = "/";
                RoditeljEmailLabel.Text = "/";
            }
        }

        private void PrikaziDokumentaciju(int idZahteva)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajDokumentacijuZaZahtev", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komanda;
            adapter.Fill(podaciDataSet);

            konekcija.Close();
            konekcija.Dispose();

            DokumentacijaGridView.DataSource = podaciDataSet.Tables[0];
            DokumentacijaGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idZahteva = int.Parse(Request.QueryString["IDZahteva"]);

                PrikaziZahtevZaStampu(idZahteva);
                PrikaziRoditeljaStaratelja(idZahteva);
                PrikaziDokumentaciju(idZahteva);
            }
        }
    }
}