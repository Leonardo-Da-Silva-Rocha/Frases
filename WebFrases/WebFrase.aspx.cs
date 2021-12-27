using System;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class WebFrase : System.Web.UI.Page
    {
        private Frase frase = new Frase();
        private DalFrase dalFrase = new DalFrase();
        private DalAutor dalAutor = new DalAutor();
        private DalCategoria dalCategoria = new DalCategoria();
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
            if (!IsPostBack)
            {
                AtualizarAutor();
                AtualizarCategoria();
            }
        }

        private void AtualizarGrid()
        {
            try
            {
                GridView1.DataSource = dalFrase.Listar();
                GridView1.DataBind();
            }
            catch
            {
                Response.Write("<script>alert('Erro ao atualizar grid')</script>");
            }
        }

        private void AtualizarAutor()
        {
            try
            {
                dpAutor.DataSource = dalAutor.Listar();
                dpAutor.DataTextField = "nome";
                dpAutor.DataValueField = "id";
                dpAutor.DataBind();
            }
            catch
            {
                Response.Write("<script>alert('Erro ao atualizar autores')</script>");
            }
        }

        private void AtualizarCategoria()
        {
            try
            {
                dpCategoria.DataSource = dalCategoria.Listar();
                dpCategoria.DataTextField = "descricao";
                dpCategoria.DataValueField = "id";
                dpCategoria.DataBind();
            }
            catch
            {
                Response.Write("<script>alert('Erro ao atualizar categoria')</script>");
            }
        }

        private void LimparCampos()
        {
            this.txtFrase.Text = "";
            this.txtId.Text = "";
            this.btnInserir.Text = "Inserir";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            this.GridView1.SelectedIndex = -1;
        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            InserirAlterar();
        }

        private void InserirAlterar()
        {
            this.frase.Texto = this.txtFrase.Text;
            this.frase.Autor = Convert.ToInt32(dpAutor.SelectedValue);
            this.frase.Categoria = Convert.ToInt32(dpCategoria.SelectedValue);

            try
            {
                if (btnInserir.Text.Equals("Inserir"))
                {
                    dalFrase.Inserir(this.frase);
                    Response.Write("<script>alert('Frase inserida com sucesso !')</script>");
                }
                else
                {
                    frase.Id = Convert.ToInt32(this.txtId.Text);
                    dalFrase.Alterar(frase);
                    Response.Write("<script>alert('Frase alterada com sucesso !')</script>");
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "')</script>");
            }
            AtualizarGrid();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Categoria c = new Categoria();
                Autor a = new Autor();
                c = dalCategoria.GetRegistro(Convert.ToInt32(GridView1.Rows[GridView1.SelectedIndex].Cells[5].Text));
                a = dalAutor.GetAutor(Convert.ToInt32(GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text));
                dpCategoria.SelectedValue = a.Id.ToString();
                dpAutor.SelectedValue = c.Id.ToString();

                this.txtId.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text;
                this.txtFrase.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
                btnInserir.Text = "Alterar";
            }
            catch
            {
                Response.Write("<scrip>alert('Erro ao selecionar')</script>");
            }
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                int id = Convert.ToInt32(GridView1.Rows[index].Cells[2].Text);
                dalFrase.Deletar(id);
                AtualizarGrid();
                Response.Write("<script>alert('Frase deletada com sucesso !')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        
    }
}