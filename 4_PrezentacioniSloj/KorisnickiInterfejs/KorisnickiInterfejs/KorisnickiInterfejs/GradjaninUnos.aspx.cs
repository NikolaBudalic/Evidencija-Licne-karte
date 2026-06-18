using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace KorisnickiInterfejs
{
    public partial class GradjaninUnos : System.Web.UI.Page
    {
        private string StringKonekcije
        {
            get { return ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString; }
        }

        private void IsprazniKontrole()
        {
            JMBGTextBox.Text = "";
            ImeTextBox.Text = "";
            PrezimeTextBox.Text = "";
            DatumRodjenjaTextBox.Text = "";
            DrzavljanstvoTextBox.Text = "";
            AdresaTextBox.Text = "";
            TelefonTextBox.Text = "";
            EmailTextBox.Text = "";
            BrojStareLKTextBox.Text = "";
            DatumIstekaLKTextBox.Text = "";
            StatusLabel.Text = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OdustaniButton_Click(object sender, EventArgs e)
        {
            IsprazniKontrole();
        }

        protected void SnimiButton_Click(object sender, EventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DodajGradjanina", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;

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
                StatusLabel.Text = "Uspešno snimljen građanin.";
                IsprazniKontrole();
            }
            else
            {
                StatusLabel.Text = "Građanin nije snimljen.";
            }
        }
    }
}