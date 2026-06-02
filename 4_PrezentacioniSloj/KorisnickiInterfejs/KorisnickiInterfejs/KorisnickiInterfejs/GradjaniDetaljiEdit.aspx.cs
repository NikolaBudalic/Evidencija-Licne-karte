using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace KorisnickiInterfejs
{
    public partial class GradjaniDetaljiEdit : System.Web.UI.Page
    {
        private string stariJMBG;

        private string StringKonekcije
        {
            get { return ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString; }
        }

        private void AktivirajKontrole()
        {
            JMBGTextBox.Enabled = true;
            ImeTextBox.Enabled = true;
            PrezimeTextBox.Enabled = true;
            DatumRodjenjaTextBox.Enabled = true;
            PolDropDownList.Enabled = true;
            DrzavljanstvoTextBox.Enabled = true;
            AdresaTextBox.Enabled = true;
            TelefonTextBox.Enabled = true;
            EmailTextBox.Enabled = true;
            BrojStareLKTextBox.Enabled = true;
            DatumIstekaLKTextBox.Enabled = true;
        }

        private void DeaktivirajKontrole()
        {
            JMBGTextBox.Enabled = false;
            ImeTextBox.Enabled = false;
            PrezimeTextBox.Enabled = false;
            DatumRodjenjaTextBox.Enabled = false;
            PolDropDownList.Enabled = false;
            DrzavljanstvoTextBox.Enabled = false;
            AdresaTextBox.Enabled = false;
            TelefonTextBox.Enabled = false;
            EmailTextBox.Enabled = false;
            BrojStareLKTextBox.Enabled = false;
            DatumIstekaLKTextBox.Enabled = false;
        }

        private void PrikaziGradjanina(string jmbg)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajGradjaninaPoJMBG", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@JMBG", SqlDbType.Char).Value = jmbg;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komanda;
            adapter.Fill(podaciDataSet);

            konekcija.Close();
            konekcija.Dispose();

            if (podaciDataSet.Tables[0].Rows.Count > 0)
            {
                DataRow red = podaciDataSet.Tables[0].Rows[0];

                JMBGTextBox.Text = red["JMBG"].ToString();
                ImeTextBox.Text = red["Ime"].ToString();
                PrezimeTextBox.Text = red["Prezime"].ToString();

                DatumRodjenjaTextBox.Text =
                    Convert.ToDateTime(red["DatumRodjenja"]).ToString("yyyy-MM-dd");

                PolDropDownList.SelectedValue = red["Pol"].ToString();
                DrzavljanstvoTextBox.Text = red["Drzavljanstvo"].ToString();
                AdresaTextBox.Text = red["AdresaPrebivalista"].ToString();
                TelefonTextBox.Text = red["KontaktTelefon"].ToString();
                EmailTextBox.Text = red["Email"].ToString();
                BrojStareLKTextBox.Text = red["BrojStareLK"].ToString();

                if (red["DatumIstekaLK"] != DBNull.Value)
                {
                    DatumIstekaLKTextBox.Text =
                        Convert.ToDateTime(red["DatumIstekaLK"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    DatumIstekaLKTextBox.Text = "";
                }
            }
            else
            {
                StatusLabel.Text = "Građanin nije pronađen.";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                stariJMBG = Request.QueryString["JMBG"];
                ViewState["StariJMBG"] = stariJMBG;

                PrikaziGradjanina(stariJMBG);
                DeaktivirajKontrole();
            }
        }

        protected void IzmeniButton_Click(object sender, EventArgs e)
        {
            AktivirajKontrole();
            StatusLabel.Text = "Izmena je omogućena.";
        }

        protected void SnimiIzmenuButton_Click(object sender, EventArgs e)
        {
            string stariJMBG = ViewState["StariJMBG"].ToString();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("IzmeniGradjanina", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;

            komanda.Parameters.Add("@StariJMBG", SqlDbType.Char).Value = stariJMBG;
            komanda.Parameters.Add("@JMBG", SqlDbType.Char).Value = JMBGTextBox.Text;
            komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = ImeTextBox.Text;
            komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = PrezimeTextBox.Text;
            komanda.Parameters.Add("@DatumRodjenja", SqlDbType.Date).Value = DateTime.Parse(DatumRodjenjaTextBox.Text);
            komanda.Parameters.Add("@Pol", SqlDbType.NVarChar).Value = PolDropDownList.SelectedValue;
            komanda.Parameters.Add("@Drzavljanstvo", SqlDbType.NVarChar).Value = DrzavljanstvoTextBox.Text;
            komanda.Parameters.Add("@AdresaPrebivalista", SqlDbType.NVarChar).Value = AdresaTextBox.Text;
            komanda.Parameters.Add("@KontaktTelefon", SqlDbType.NVarChar).Value = TelefonTextBox.Text;
            komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = EmailTextBox.Text;
            komanda.Parameters.Add("@BrojStareLK", SqlDbType.NVarChar).Value = BrojStareLKTextBox.Text;

            if (string.IsNullOrWhiteSpace(DatumIstekaLKTextBox.Text))
            {
                komanda.Parameters.Add("@DatumIstekaLK", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                komanda.Parameters.Add("@DatumIstekaLK", SqlDbType.Date).Value = DateTime.Parse(DatumIstekaLKTextBox.Text);
            }

            int brojSlogova = komanda.ExecuteNonQuery();

            konekcija.Close();
            konekcija.Dispose();

            if (brojSlogova > 0)
            {
                ViewState["StariJMBG"] = JMBGTextBox.Text;
                StatusLabel.Text = "Uspešno izmenjen građanin.";
                DeaktivirajKontrole();
            }
            else
            {
                StatusLabel.Text = "Izmena nije izvršena.";
            }
        }

        protected void ObrisiButton_Click(object sender, EventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("ObrisiGradjanina", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@JMBG", SqlDbType.Char).Value = JMBGTextBox.Text;

            int brojSlogova = komanda.ExecuteNonQuery();

            konekcija.Close();
            konekcija.Dispose();

            if (brojSlogova > 0)
            {
                Response.Redirect("GradjaniTabelarni.aspx");
            }
            else
            {
                StatusLabel.Text = "Građanin nije obrisan. Proveri da li ima vezane zahteve.";
            }
        }
    }
}