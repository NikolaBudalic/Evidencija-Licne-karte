using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KorisnickiInterfejs
{
    public partial class ZahteviDetaljiEdit : System.Web.UI.Page
    {
        private string StringKonekcije
        {
            get { return ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString; }
        }

        private void DeaktivirajKontrole()
        {
            StatusZahtevaDropDownList.Enabled = false;
            NapomenaTextBox.Enabled = false;
        }

        private void AktivirajKontrole()
        {
            StatusZahtevaDropDownList.Enabled = true;
            NapomenaTextBox.Enabled = true;
        }

        private void PrikaziPodatke(int idZahteva)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajDetaljeZahtevaPoID", konekcija);
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

                IDZahtevaTextBox.Text = red["IDZahteva"].ToString();
                JMBGGradjaninaTextBox.Text = red["JMBGGradjanina"].ToString();
                ImePrezimeTextBox.Text = red["Ime"] + " " + red["Prezime"];
                DatumPodnosenjaTextBox.Text = Convert.ToDateTime(red["DatumPodnosenja"]).ToString("dd.MM.yyyy");
                RazlogIzdavanjaTextBox.Text = red["RazlogIzdavanja"].ToString();
                TipZahtevaTextBox.Text = red["TipZahteva"].ToString();
                MestoPodnosenjaTextBox.Text = red["MestoPodnosenja"].ToString();
                StatusZahtevaDropDownList.SelectedValue = red["StatusZahteva"].ToString();
                NapomenaTextBox.Text = red["Napomena"].ToString();
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

        private void PrikaziIstorijuStatusa(int idZahteva)
        {
            DataSet podaciDataSet = new DataSet();

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand("DajIstorijuStatusaZaZahtev", konekcija);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = komanda;
            adapter.Fill(podaciDataSet);

            konekcija.Close();
            konekcija.Dispose();

            IstorijaStatusaGridView.DataSource = podaciDataSet.Tables[0];
            IstorijaStatusaGridView.DataBind();
        }

        private bool DaLiSuSvaDokumentaDostavljena(int idZahteva)
        {
            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand(
                @"SELECT COUNT(*) 
                  FROM Dokumentacija
                  WHERE IDZahteva = @IDZahteva AND Dostavljeno = 0",
                konekcija);

            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            int brojNedostavljenih = Convert.ToInt32(komanda.ExecuteScalar());

            konekcija.Close();
            konekcija.Dispose();

            return brojNedostavljenih == 0;
        }

        private string GenerisiBrojNoveLK()
        {
            int sledeciBroj = 1;

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand(
                @"SELECT ISNULL(MAX(CAST(REPLACE(BrojNoveLK, 'LK-', '') AS INT)), 0) + 1
                  FROM Zahtev
                  WHERE BrojNoveLK IS NOT NULL
                    AND BrojNoveLK LIKE 'LK-%'",
                konekcija);

            object rezultat = komanda.ExecuteScalar();

            if (rezultat != null && rezultat != DBNull.Value)
            {
                sledeciBroj = Convert.ToInt32(rezultat);
            }

            konekcija.Close();
            konekcija.Dispose();

            return "LK-" + sledeciBroj.ToString("000000");
        }

        private void DodeliNovuLicnuKartuAkoNePostoji(int idZahteva)
        {
            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand proveraKomanda = new SqlCommand(
                @"SELECT BrojNoveLK
                  FROM Zahtev
                  WHERE IDZahteva = @IDZahteva",
                konekcija);

            proveraKomanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            object postojeciBroj = proveraKomanda.ExecuteScalar();

            if (postojeciBroj == DBNull.Value || postojeciBroj == null || postojeciBroj.ToString() == "")
            {
                string brojNoveLK = GenerisiBrojNoveLK();

                SqlCommand updateKomanda = new SqlCommand(
                    @"UPDATE Zahtev
                      SET BrojNoveLK = @BrojNoveLK,
                          DatumIstekaNoveLK = @DatumIstekaNoveLK
                      WHERE IDZahteva = @IDZahteva",
                    konekcija);

                updateKomanda.Parameters.Add("@BrojNoveLK", SqlDbType.NVarChar).Value = brojNoveLK;
                updateKomanda.Parameters.Add("@DatumIstekaNoveLK", SqlDbType.Date).Value = DateTime.Now.AddYears(10);
                updateKomanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

                updateKomanda.ExecuteNonQuery();
            }

            konekcija.Close();
            konekcija.Dispose();
        }

        private void PromeniStatus(int idZahteva, string noviStatus, string napomena)
        {
            string stariStatus = "";

            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komandaStariStatus = new SqlCommand(
                "SELECT StatusZahteva FROM Zahtev WHERE IDZahteva = @IDZahteva",
                konekcija);

            komandaStariStatus.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            object rezultat = komandaStariStatus.ExecuteScalar();

            if (rezultat != null)
            {
                stariStatus = rezultat.ToString();
            }

            SqlCommand komandaUpdate = new SqlCommand(
                @"UPDATE Zahtev
                  SET StatusZahteva = @StatusZahteva,
                      Napomena = @Napomena
                  WHERE IDZahteva = @IDZahteva",
                konekcija);

            komandaUpdate.Parameters.Add("@StatusZahteva", SqlDbType.NVarChar).Value = noviStatus;
            komandaUpdate.Parameters.Add("@Napomena", SqlDbType.NVarChar).Value = napomena;
            komandaUpdate.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;

            komandaUpdate.ExecuteNonQuery();

            if (stariStatus != noviStatus)
            {
                SqlCommand komandaIstorija = new SqlCommand("DodajIstorijuStatusa", konekcija);
                komandaIstorija.CommandType = CommandType.StoredProcedure;

                komandaIstorija.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = idZahteva;
                komandaIstorija.Parameters.Add("@StariStatus", SqlDbType.NVarChar).Value = stariStatus;
                komandaIstorija.Parameters.Add("@NoviStatus", SqlDbType.NVarChar).Value = noviStatus;
                komandaIstorija.Parameters.Add("@Korisnik", SqlDbType.NVarChar).Value = "admin";
                komandaIstorija.Parameters.Add("@Napomena", SqlDbType.NVarChar).Value = napomena;

                komandaIstorija.ExecuteNonQuery();
            }

            konekcija.Close();
            konekcija.Dispose();

            if (noviStatus == "Odobren")
            {
                DodeliNovuLicnuKartuAkoNePostoji(idZahteva);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idZahteva = int.Parse(Request.QueryString["IDZahteva"]);

                PrikaziPodatke(idZahteva);
                PrikaziDokumentaciju(idZahteva);
                PrikaziIstorijuStatusa(idZahteva);
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
            int idZahteva = int.Parse(IDZahtevaTextBox.Text);

            PromeniStatus(
                idZahteva,
                StatusZahtevaDropDownList.SelectedValue,
                NapomenaTextBox.Text);

            StatusLabel.Text = "Uspešno izmenjen zahtev.";
            DeaktivirajKontrole();

            PrikaziPodatke(idZahteva);
            PrikaziIstorijuStatusa(idZahteva);
        }

        protected void ObrisiButton_Click(object sender, EventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            SqlCommand komanda = new SqlCommand(
                "DELETE FROM Zahtev WHERE IDZahteva = @IDZahteva",
                konekcija);

            komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value =
                int.Parse(IDZahtevaTextBox.Text);

            int brojSlogova = komanda.ExecuteNonQuery();

            konekcija.Close();
            konekcija.Dispose();

            if (brojSlogova > 0)
            {
                Response.Redirect("ZahteviTabelarni.aspx");
            }
            else
            {
                StatusLabel.Text = "Brisanje nije izvršeno.";
            }
        }

        protected void SacuvajDokumentacijuButton_Click(object sender, EventArgs e)
        {
            SqlConnection konekcija = new SqlConnection(StringKonekcije);
            konekcija.Open();

            foreach (GridViewRow red in DokumentacijaGridView.Rows)
            {
                int idDokumentacije =
                    Convert.ToInt32(DokumentacijaGridView.DataKeys[red.RowIndex].Value);

                CheckBox dostavljenoCheckBox =
                    (CheckBox)red.FindControl("DostavljenoCheckBox");

                SqlCommand komanda = new SqlCommand("AzurirajDokumentaciju", konekcija);
                komanda.CommandType = CommandType.StoredProcedure;

                komanda.Parameters.Add("@IDDokumentacije", SqlDbType.Int).Value =
                    idDokumentacije;

                komanda.Parameters.Add("@Dostavljeno", SqlDbType.Bit).Value =
                    dostavljenoCheckBox.Checked;

                komanda.ExecuteNonQuery();
            }

            konekcija.Close();
            konekcija.Dispose();

            int idZahteva = int.Parse(IDZahtevaTextBox.Text);

            string trenutniStatus = StatusZahtevaDropDownList.SelectedValue;

            if (trenutniStatus != "Odbijen")
            {
                if (DaLiSuSvaDokumentaDostavljena(idZahteva))
                {
                    PromeniStatus(
                        idZahteva,
                        "Odobren",
                        "Sva dokumentacija je dostavljena.");
                }
                else
                {
                    PromeniStatus(
                        idZahteva,
                        "Na proveri",
                        "Dokumentacija nije u potpunosti dostavljena.");
                }
            }

            PrikaziPodatke(idZahteva);
            PrikaziDokumentaciju(idZahteva);
            PrikaziIstorijuStatusa(idZahteva);

            StatusLabel.Text = "Dokumentacija je uspešno ažurirana.";
        }
    }
}