using System;
using System.Data;
using System.Data.SqlClient;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DalCategoria
    {
        private static SqlConnection conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        public void Inserir(Categoria obj)
        {
            try
            {
                SqlCommand cmd = conexao.CreateCommand();
                cmd.Connection = conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Categorias (descricao) VALUES (@descricao);select @@IDENTITY";
                conexao.Open();
                cmd.Parameters.AddWithValue("@descricao", obj.Descricao);
                obj.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch
            {
                throw new Exception("Erro ao inserir categoria");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Alterar(Categoria obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Categorias SET descricao = @descricao WHERE Id = @id";
                cmd.Parameters.AddWithValue("id", obj.Id);
                cmd.Parameters.AddWithValue("descricao", obj.Descricao);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao alterar categoria");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Excluir(int cod)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Categorias WHERE Id = @id";
                cmd.Parameters.AddWithValue("id", cod);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao excluir");
            }
            finally
            {
                conexao.Close();
            }
        }

        public DataTable Listar()
        {
            DataTable tabela = new DataTable();
            
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Categorias", conexao.ConnectionString);

            try
            {
                da.Fill(tabela);
                return tabela;
            }
            catch
            {
                throw new Exception("Erro ao listar");
            }
        }

        public DataTable Localizar(String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from categoria where categoria like '%" +
                valor + "%'", conexao.ConnectionString);

            try
            {
                da.Fill(tabela);
                return tabela;
            }
            catch
            {
                throw new Exception("Erro ao localizar");
            }
        }

        public Categoria GetRegistro(int id)
        {
            Categoria obj = new Categoria();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conexao;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Categorias where id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                conexao.Open();
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.HasRows)
                {
                    registro.Read();
                    obj.Id = Convert.ToInt32(registro["id"]);
                    obj.Descricao = Convert.ToString(registro["descricao"]);
                }
                conexao.Close();

            }
            catch
            {
                throw new Exception("Erro ao listar pelo id");
            }
            finally
            {
                conexao.Close();
            }
            return obj;
        }
    }
}