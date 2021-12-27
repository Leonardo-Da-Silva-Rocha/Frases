using System;
using WebFrases.DAL;
using WebFrases.MODELO;
using System.IO;
namespace WebFrases
{
    public partial class WebAutor : System.Web.UI.Page
    {
        private DalAutor dalAutor = new DalAutor();
        private Autor autor = new Autor();

        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            autor.Nome = txtNome.Text;
            autor.Foto = "";
            try
            {
                if (btnSalvar.Text.Equals("Inserir"))
                {
                    SalvarImagem();
                    Inserir();
                }
                else
                {
                    int idAutor = Convert.ToInt32(txtId.Text);
                    DeletarImagem(idAutor);
                    SalvarImagem();
                    Alterar();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            AtualizarGrid();
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            btnSalvar.Text = "Inserir";
        }

        private void AtualizarGrid()
        {
            try
            {
                GridView1.DataSource = dalAutor.Listar();
                GridView1.DataBind();
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "')</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            btnSalvar.Text = "Inserir";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;
            this.txtId.Text = GridView1.Rows[index].Cells[2].Text;
            this.txtNome.Text = GridView1.Rows[index].Cells[3].Text;
            btnSalvar.Text = "Alterar";
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {

                int index = e.RowIndex;
                int id = Convert.ToInt32(GridView1.Rows[index].Cells[2].Text);
                DeletarImagem(id);
                dalAutor.Deletar(id);
                Response.Write("<script>alert('Autor Deletado')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            AtualizarGrid();
        }

        private void Inserir()
        {
            try
            {
                dalAutor.Inserir(autor);
                Response.Write("<script>alert('Autor cadastrado')</script>");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void Alterar()
        {
            try
            {
                autor.Id = Convert.ToInt32(this.txtId.Text);
                dalAutor.Alterar(autor);
                Response.Write("<script>alert('Autor Alterado')</script>");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SalvarImagem()
        {
            String caminho = Server.MapPath(@"IMAGENS\AUTORES\");
            try
            {
                if (FpFoto.PostedFile.FileName != "")
                {
                    autor.Foto = DateTime.Now.Millisecond.ToString() + FpFoto.PostedFile.FileName;
                    String img = caminho + autor.Foto;
                    FpFoto.PostedFile.SaveAs(img);
                }
            }
            catch
            {
                throw new Exception("Erro ao salvar imagem");
            }
        }

        private void DeletarImagem(int idAutor)
        {
            String caminho = Server.MapPath(@"IMAGENS\AUTORES\");
            try
            {
                autor = dalAutor.GetAutor(idAutor);
                if (autor.Foto != "")
                {
                    File.Delete(caminho + autor.Foto);
                }
            }
            catch
            {
                throw new Exception("Erro ao deletar imagem");
            }
        }
    }
}