using System;
using System.Linq;
using System.Web.Script.Serialization;
using KorisnickiInterfejs.ModelsEF;

namespace KorisnickiInterfejs
{
    public partial class GradjaniApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "application/json";

            using (LicnaKartaDbContext db = new LicnaKartaDbContext())
            {
                var gradjani = db.Gradjani
                    .Select(g => new
                    {
                        g.JMBG,
                        g.Ime,
                        g.Prezime,
                        g.DatumRodjenja,
                        g.Pol,
                        g.Drzavljanstvo,
                        g.AdresaPrebivalista,
                        g.KontaktTelefon,
                        g.Email,
                        g.BrojStareLK,
                        g.DatumIstekaLK
                    })
                    .ToList();

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string json = serializer.Serialize(gradjani);

                Response.Write(json);
            }

            Response.End();
        }
    }
}