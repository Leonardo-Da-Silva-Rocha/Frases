using System;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class WebLogin : System.Web.UI.Page
    {
        private Usuario usuario = new Usuario();
        private DalUsuario dal = new DalUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnlLogar_Click(object sender, EventArgs e)
        {
            try
            {
                usuario = dal.GetEmail(txtLogin.Text);
                if (usuario.Senha != null)
                {
                    if (usuario.Senha.Equals(txtSenha.Text) && usuario.Email.Equals(txtLogin.Text))
                    {
                        Session["Email"] = usuario.Email;
                        Session["Senha"] = usuario.Senha;
                        Session["Id"] = usuario.Id;
                        Session["Nome"] = usuario.Nome;
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Erro ao logar, e-mail ou senha invalida')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Erro ao logar, e-mail ou senha invalida')</script>");
                }
            }
            catch
            {
                Response.Write("<script>alert('Erro ao logar')</script>");
            }
        }
    }
}