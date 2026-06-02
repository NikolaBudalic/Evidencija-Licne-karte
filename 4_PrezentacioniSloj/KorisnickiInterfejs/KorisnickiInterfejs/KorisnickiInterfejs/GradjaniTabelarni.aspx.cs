using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace KorisnickiInterfejs
{
    public partial class GradjaniTabelarni : System.Web.UI.Page
    {
        private string StringKonekcije
        {
            get { return ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString; }
        }

        private void NapuniGrid(DataSet podaciDataSet)
        {
            GradjaniGridView.DataSource = podaciDataSet.Tables[0];
            GradjaniGridView.DataBind();
        }

        private DataSet DajGradjane(string filter)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda;

            if (string.IsNullOrWhiteSpace(filter))
            {
                komanda = new SqlCommand("DajSveGradjane", konekcija);
                komanda.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                komanda = new SqlCommand("DajGradjanePoFilteru", konekcija);
                komanda.CommandType = CommandType.StoredProcedure;
                komanda.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = filter;
            }

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komanda;
            adapter.Fill(podaciDataSet);

            konekcija.Close();
            konekcija.Dispose();

            return podaciDataSet;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NapuniGrid(DajGradjane(""));
            }
        }

        protected void FiltrirajButton_Click(object sender, EventArgs e)
        {
            NapuniGrid(DajGradjane(FilterTextBox.Text));
        }

        protected void SviButton_Click(object sender, EventArgs e)
        {
            FilterTextBox.Text = "";
            NapuniGrid(DajGradjane(""));
        }
    }
}