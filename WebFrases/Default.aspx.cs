using System;

namespace WebFrases
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] != null)
            {
                this.lblNome.Text = Session["Nome"].ToString();
                this.lblEmail.Text = Session["Email"].ToString();
            }
        }
    }
}