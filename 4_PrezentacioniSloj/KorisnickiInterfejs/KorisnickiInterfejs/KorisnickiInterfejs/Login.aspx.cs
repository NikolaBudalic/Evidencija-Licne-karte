using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//
using PrezentacionaLogika;
using System.Configuration;

namespace KorisnickiInterfejs
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
                      
        protected void PrijavaButton_Click(object sender, EventArgs e)
        {
            // provera korisnika 
            FormaLoginKlasa FormaLoginObjekat = new FormaLoginKlasa(ConfigurationManager.ConnectionStrings["NasaKonekcija"].ConnectionString);
            FormaLoginObjekat.KorisnickoIme = KorisnickoImeTextBox.Text;
            FormaLoginObjekat.Sifra = SifraTextBox.Text;
            bool pronadjenKorisnik = FormaLoginObjekat.VazeciKorisnik();

            if (pronadjenKorisnik)
            {
                // TO DO
                string imePrezime = FormaLoginObjekat.DajImePrezimeKorisnika();

                // ubacivanje korisnika u sesiju
                Session.Add("KorisnikImePrezime", imePrezime);

                // ucitavanje Welcome admin
                Response.Redirect("WelcomeAdmin.aspx");
            }
            else
            {
                lblStatus.Text = "KORISNIK NIJE PRONADJEN!";
            }
        }

        protected void OdustaniButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}