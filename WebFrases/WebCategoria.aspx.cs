using System;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class WebCategoria : System.Web.UI.Page
    {
        private DalCategoria dalCategoria = new DalCategoria();
        private Categoria categoria = new Categoria();
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            btnSalvar.Text = "Inserir";
            GridView1.SelectedIndex = -1;
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            btnSalvar.Text = "Inserir";
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            categoria = new Categoria();
            categoria.Descricao = txtNome.Text;
            try
            {
                if (btnSalvar.Text.Equals("Inserir"))
                {
                    dalCategoria.Inserir(categoria);
                    Response.Write("<script>alert('Categoria cadastrada')</script>");
                }
                else
                {
                    categoria.Id = Convert.ToInt32(this.txtId.Text);
                    dalCategoria.Alterar(categoria);
                    Response.Write("<script>alert('Categoria alterada')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            AtualizarGrid();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;
            this.txtId.Text = GridView1.Rows[index].Cells[2].Text;
            this.txtNome.Text = GridView1.Rows[index].Cells[3].Text;
            this.btnSalvar.Text = "Alterar";
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                int id = Convert.ToInt32(GridView1.Rows[index].Cells[2].Text);
                dalCategoria.Excluir(id);
                AtualizarGrid();
                Response.Write("<script>alert('Categoria deletada')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        private void AtualizarGrid()
        {
            try
            {
                GridView1.DataSource = dalCategoria.Listar();
                GridView1.DataBind();
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "')</script>");
            }
        }
    }
}