using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace KorisnickiInterfejs
{
    public partial class ZahteviStampaLista : System.Web.UI.Page
    {
        private string StringKonekcije
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString;
            }
        }

        private void PrikaziZahtevePoStatusu(string status)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajZahteveZaStampuPoStatusu", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@StatusZahteva", SqlDbType.NVarChar).Value = status;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komanda;
            adapter.Fill(podaciDataSet);

            konekcija.Close();
            konekcija.Dispose();

            SpisakZahtevaGridView.DataSource = podaciDataSet.Tables[0];
            SpisakZahtevaGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string status = Request.QueryString["status"];

                if (!string.IsNullOrEmpty(status))
                {
                    PrikaziZahtevePoStatusu(status);
                }
            }
        }
    }
}