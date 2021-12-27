using System;
using System.Web.UI.WebControls;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class WebUsuario : System.Web.UI.Page
    {
        private Usuario usuario = new Usuario();
        private DalUsuario Dalusuario = new DalUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                InserirAlterar();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtEmail.Text = "";
            txtNome.Text = "";
            txtSenha.Text = "";
            btnSalvar.Text = "Inserir";
        }

        private void InserirAlterar()
        {
            usuario.Nome = txtNome.Text;
            usuario.Email = txtEmail.Text;
            usuario.Senha = txtSenha.Text;
            Usuario validaUsuario = new Usuario();
            validaUsuario = Dalusuario.GetEmail(txtEmail.Text);
            try
            {
                if (btnSalvar.Text.Equals("Inserir"))
                {
                    if (validaUsuario.Id == 0)
                    {
                        Dalusuario.Inserir(usuario);
                        Response.Write("<script>alert('Usúario inserido com sucesso !')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Erro ao cadastrar usúario e-mail já cadastrado')</script>");
                    }

                }
                else
                {
                    if (validaUsuario.Id != 0)
                    {
                        usuario.Id = Convert.ToInt32(this.txtId.Text);
                        Dalusuario.Alterar(usuario);
                        Response.Write("<script>alert('Usúario alterado com sucesso !')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('E-mail invalido')</script>");
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            AtualizarGrid();
            LimparCampos();
        }

        private void AtualizarGrid()
        {
            try
            {
                this.GridView1.DataSource = Dalusuario.Listar();
                this.GridView1.DataBind();
            }
            catch
            {
                Response.Write("<script>alert('Erro ao carregar grid de usúarios')</script>");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtId.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text;
            this.txtNome.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
            this.txtEmail.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text;
            this.btnSalvar.Text = "Alterar";
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                int id = Convert.ToInt32(GridView1.Rows[index].Cells[2].Text);
                Dalusuario.Deletar(id);
                Response.Write("<script>alert('Usúario deletado')</script>");
            }
            catch
            {
                Response.Write("<script>alert('Erro ao excluir usúario')</script>");
            }
            AtualizarGrid();
        }
    }
}