using System;
using System.Web.UI;

namespace KorisnickiInterfejs
{
    public partial class ZahteviParametarStampe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FilterStampaButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(
                "ZahteviStampaLista.aspx?status=" +
                StatusDropDownList.SelectedValue);
        }
    }
}