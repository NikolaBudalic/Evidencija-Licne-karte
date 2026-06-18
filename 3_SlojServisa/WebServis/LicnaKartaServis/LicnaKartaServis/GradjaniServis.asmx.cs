using System;
using System.Collections.Generic;
using System.Web.Services;

namespace LicnaKartaServis
{
    [WebService(Namespace = "http://licnakarta.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class GradjaniServis : System.Web.Services.WebService
    {
        [WebMethod]
        public string OpisServisa()
        {
            return "Servis za sistem izdavanja ličnih karata.";
        }

        [WebMethod]
        public string DajStatusServisa()
        {
            return "Servis je aktivan.";
        }

        [WebMethod]
        public DateTime DajTrenutniDatum()
        {
            return DateTime.Now;
        }
    }
}