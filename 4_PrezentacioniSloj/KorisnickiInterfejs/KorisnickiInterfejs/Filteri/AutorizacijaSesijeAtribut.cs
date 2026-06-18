using System.Web;
using System.Web.Mvc;

namespace LicnaKarta.Filteri
{
    public class AutorizacijaSesijeAtribut : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["IDKorisnika"] != null;
        }

        protected override void HandleUnauthorizedRequest(
            AuthorizationContext filterContext)
        {
            filterContext.Result =
                new RedirectResult("/Account/Prijava");
        }
    }
}