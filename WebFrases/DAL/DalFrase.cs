using System;
using System.Data;
using System.Data.SqlClient;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DalFrase
    {
        private static SqlConnection conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Inserir(Frase frase)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO Frases (frase,autor,categoria) VALUES (@frase,@autor,@categoria);SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@frase", frase.Texto);
                cmd.Parameters.AddWithValue("@autor", frase.Autor);
                cmd.Parameters.AddWithValue("@categoria", frase.Categoria);
                conexao.Open();
                frase.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir frase");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Alterar(Frase frase)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE Frases SET frase=@frase, autor=@autor, categoria=@categoria WHERE Id=@id";
                cmd.Parameters.AddWithValue("@frase", frase.Texto);
                cmd.Parameters.AddWithValue("@autor", frase.Autor);
                cmd.Parameters.AddWithValue("@categoria", frase.Categoria);
                cmd.Parameters.AddWithValue("@id", frase.Id);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao alterar frase");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Deletar(int idFrase)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM Frases WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", idFrase);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao deletar frase");
            }
            finally
            {
                conexao.Close();
            }
        }

        public Frase GetFrase(int idFrase)
        {
            Frase frase = new Frase();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Frases WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", idFrase);
                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    frase.Id = Convert.ToInt32(reader["Id"]);
                    frase.Texto = reader["frase"].ToString();
                    frase.Autor = Convert.ToInt32(reader["autor"]);
                    frase.Categoria = Convert.ToInt32(reader["categoria"]);
                }
            }
            catch
            {
                throw new Exception("Erro ao listar frase por id");
            }
            finally
            {
                conexao.Close();
            }
            return frase;
        }

        public DataTable Listar()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Frases inner join Autores on Autores.Id = Frases.autor inner join Categorias on Categorias.Id = Frases.categoria", conexao.ConnectionString);
            try
            {
                adapter.Fill(tabela);
                return tabela;
            }
            catch
            {
                throw new Exception("Erro ao listar frase");
            }
        }
    }
}