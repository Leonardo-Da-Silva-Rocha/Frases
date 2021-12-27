using System;

namespace WebFrases
{
    public partial class PaginaMestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/WebLogin.aspx");
            }
        }
    }
}