using System;
using System.Data;
using System.Data.SqlClient;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DalUsuario
    {
        private static SqlConnection conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public void Inserir(Usuario usuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO Usuarios (nome,email,senha) VALUES (@nome,@email,@senha);SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                conexao.Open();
                usuario.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch
            {
                throw new Exception("Erro ao inserir usuario");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Alterar(Usuario usuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE Usuarios SET nome=@nome, email=@email, senha=@senha WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", usuario.Id);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao alterar usuario");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Deletar(int idUsuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM Usuarios WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", idUsuario);
                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao excluir usuario");
            }
            finally
            {
                conexao.Close();
            }
        }

        public Usuario GetUsuario(int idUsuario)
        {
            Usuario usuario = new Usuario();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Usuarios WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", idUsuario);
                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Senha = reader["Senha"].ToString();
                }
            }
            catch
            {
                throw new Exception("Erro ao listar usuario por id ");
            }
            finally
            {
                conexao.Close();
            }
            return usuario;
        }


        public Usuario GetEmail(String email)
        {
            Usuario usuario = new Usuario();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conexao;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Usuarios WHERE email = @email";
                cmd.Parameters.AddWithValue("@email", email);
                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Senha = reader["Senha"].ToString();
                }
            }
            catch
            {
                throw new Exception("Erro ao listar usuario por id ");
            }
            finally
            {
                conexao.Close();
            }
            return usuario;
        }

        public DataTable Listar()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Usuarios", conexao.ConnectionString);
            try
            {
                da.Fill(tabela);
                return tabela;
            }
            catch
            {
                throw new Exception("Erro ao listar usuarios");
            }
        }
    }
}