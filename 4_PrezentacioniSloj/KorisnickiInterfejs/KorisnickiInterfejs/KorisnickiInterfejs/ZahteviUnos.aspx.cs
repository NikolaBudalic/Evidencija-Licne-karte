using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using PrezentacionaLogika;

namespace KorisnickiInterfejs
{
    public partial class ZahteviUnos : System.Web.UI.Page
    {
        private FormaZahtevUnosKlasa formaZahtevUnosObjekat;

        private void IsprazniKontrole()
        {
            JMBGGradjaninaTextBox.Text = "";
            DatumPodnosenjaTextBox.Text = "";
            MestoPodnosenjaTextBox.Text = "";
            NapomenaTextBox.Text = "";

            RoditeljImePrezimeTextBox.Text = "";
            RoditeljJMBGTextBox.Text = "";
            SrodstvoTextBox.Text = "";
            RoditeljTelefonTextBox.Text = "";
            RoditeljEmailTextBox.Text = "";

            StatusLabel.Text = "";
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

        private bool DaLiJeMaloletan(string jmbg)
        {
            SqlConnection konekcija = new SqlConnection(
                ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString);

            konekcija.Open();

            SqlCommand komanda = new SqlCommand(
                "SELECT DatumRodjenja FROM Gradjanin WHERE JMBG = @JMBG",
                konekcija);

            komanda.Parameters.Add("@JMBG", SqlDbType.Char).Value = jmbg;

            object rezultat = komanda.ExecuteScalar();

            konekcija.Close();
            konekcija.Dispose();

            if (rezultat == null || rezultat == DBNull.Value)
            {
                return false;
            }

            DateTime datumRodjenja = Convert.ToDateTime(rezultat);
            int godine = IzracunajGodine(datumRodjenja);

            return godine < 18;
        }

        private bool DaLiSuPopunjeniPodaciRoditelja()
        {
            return !string.IsNullOrWhiteSpace(RoditeljImePrezimeTextBox.Text)
                && !string.IsNullOrWhiteSpace(RoditeljJMBGTextBox.Text)
                && !string.IsNullOrWhiteSpace(SrodstvoTextBox.Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            formaZahtevUnosObjekat =
                new FormaZahtevUnosKlasa(
                    ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString);
        }

        protected void OdustaniButton_Click(object sender, EventArgs e)
        {
            IsprazniKontrole();
        }

        protected void SnimiButton_Click(object sender, EventArgs e)
        {
            if (DaLiJeMaloletan(JMBGGradjaninaTextBox.Text) && !DaLiSuPopunjeniPodaciRoditelja())
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "greskaRoditelj",
                    "alert('Za građanina mlađeg od 18 godina obavezno je popuniti podatke o roditelju ili staratelju.');",
                    true);

                StatusLabel.Text = "Zahtev nije snimljen. Potrebni su podaci roditelja/staratelja.";
                return;
            }

            bool uspehSnimanja = false;

            formaZahtevUnosObjekat.JMBGGradjanina = JMBGGradjaninaTextBox.Text;
            formaZahtevUnosObjekat.DatumPodnosenja = DateTime.Parse(DatumPodnosenjaTextBox.Text);
            formaZahtevUnosObjekat.RazlogIzdavanja = RazlogIzdavanjaDropDownList.SelectedValue;
            formaZahtevUnosObjekat.TipZahteva = TipZahtevaDropDownList.SelectedValue;
            formaZahtevUnosObjekat.MestoPodnosenja = MestoPodnosenjaTextBox.Text;
            formaZahtevUnosObjekat.StatusZahteva = "U obradi";
            formaZahtevUnosObjekat.Napomena = NapomenaTextBox.Text;

            formaZahtevUnosObjekat.RoditeljImePrezime = RoditeljImePrezimeTextBox.Text;
            formaZahtevUnosObjekat.RoditeljJMBG = RoditeljJMBGTextBox.Text;
            formaZahtevUnosObjekat.Srodstvo = SrodstvoTextBox.Text;
            formaZahtevUnosObjekat.RoditeljTelefon = RoditeljTelefonTextBox.Text;
            formaZahtevUnosObjekat.RoditeljEmail = RoditeljEmailTextBox.Text;

            uspehSnimanja = formaZahtevUnosObjekat.SnimiPodatke();

            if (uspehSnimanja)
            {
                StatusLabel.Text = "Uspešno snimljen zahtev!";
                IsprazniKontrole();
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "greskaSnimanje",
                    "alert('Zahtev nije snimljen. Proverite unesene podatke.');",
                    true);

                StatusLabel.Text = "Zahtev nije snimljen. Proverite unesene podatke.";
            }
        }
    }
}