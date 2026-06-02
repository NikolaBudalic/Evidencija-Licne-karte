using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using PrezentacionaLogika;

namespace KorisnickiInterfejs
{
    public partial class ZahteviTabelarni : System.Web.UI.Page
    {
        private void NapuniGrid(DataSet podaciDataSet)
        {
            SpisakZahtevaGridView.DataSource = podaciDataSet.Tables[0];
            SpisakZahtevaGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormaZahtevTabelaEditKlasa formaObjekat =
                    new FormaZahtevTabelaEditKlasa(
                        ConfigurationManager.ConnectionStrings["NasaKonekcija"].ToString());

                NapuniGrid(formaObjekat.DajPodatkeZaGrid(""));
            }
        }

        protected void FiltrirajButton_Click(object sender, EventArgs e)
        {
            FormaZahtevTabelaEditKlasa formaObjekat =
                new FormaZahtevTabelaEditKlasa(
                    ConfigurationManager.ConnectionStrings["NasaKonekcija"].ToString());

            NapuniGrid(formaObjekat.DajPodatkeZaGrid(FilterTextBox.Text));
        }

        protected void SviButton_Click(object sender, EventArgs e)
        {
            FormaZahtevTabelaEditKlasa formaObjekat =
                new FormaZahtevTabelaEditKlasa(
                    ConfigurationManager.ConnectionStrings["NasaKonekcija"].ToString());

            NapuniGrid(formaObjekat.DajPodatkeZaGrid(""));
        }
    }
}