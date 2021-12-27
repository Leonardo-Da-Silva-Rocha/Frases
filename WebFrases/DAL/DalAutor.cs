using System;
using System.Data;
using System.Data.SqlClient;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DalAutor
    {
        private static SqlConnection conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Inserir(Autor autor)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO Autores (nome,foto) VALUES (@nome,@foto);SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@nome", autor.Nome);
                cmd.Parameters.AddWithValue("@foto", autor.Foto);
                conexao.Open();
                autor.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir autor" + e.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Deletar(int idAutor)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM Autores WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", idAutor);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao deletar autor");
            }
            finally
            {
                conexao.Close();
            }
        }
        public void Alterar(Autor autor)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE Autores SET nome=@nome, foto=@foto WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", autor.Id);
                cmd.Parameters.AddWithValue("@nome", autor.Nome);
                cmd.Parameters.AddWithValue("@foto", autor.Foto);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao alterar autor");
            }
            finally
            {
                conexao.Close();
            }
        }

        public Autor GetAutor(int idAutor)
        {
            Autor autor = new Autor();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Autores WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", idAutor);
                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    autor.Id = Convert.ToInt32(reader["id"]);
                    autor.Nome = Convert.ToString(reader["nome"]);
                    autor.Foto = reader["foto"].ToString();
                }
            }
            catch
            {
                throw new Exception("Erro ao listar autor por id");
            }
            finally
            {
                conexao.Close();
            }
            return autor;
        }

        public DataTable Listar()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Autores", conexao.ConnectionString);
            try
            {
                adapter.Fill(tabela);
                return tabela;
            }
            catch
            {
                throw new Exception("Erro ao listar autor");
            }
        }
    }
}