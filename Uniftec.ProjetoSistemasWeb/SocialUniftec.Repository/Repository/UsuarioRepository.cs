using Npgsql;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Domain.Repository;

namespace SocialUniftec.Repository.Repository
{
	public class UsuarioRepository : IUsuarioRepository
	{
        private string ConnectionString;

        public UsuarioRepository(string ConnectionString = null)
        {
            this.ConnectionString = ConnectionString;
        }

        public void Alterar(Usuario usuario)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();

			using var cmd = new NpgsqlCommand(
				@"UPDATE public.usuario
					SET nome=@nome, sobrenome=@sobrenome, senha=@senha, datacomemorativa=@datacomemorativa, sexo=@sexo, 
					bio=@bio, fotodeperfil=@fotodeperfil, cidade=@cidade, uf=@uf, telefone=@telefone, documento=@documento, tipo=@tipo
					WHERE id=@id;",
				con);

			AdicionarParametrosInserirOuAlterar(usuario, cmd);
			cmd.ExecuteNonQuery();

			cmd.Parameters.Clear();
			cmd.CommandText =
				@"DELETE FROM public.amizade
					WHERE idusuario=@idusuario;";

			cmd.Parameters.AddWithValue("idusuario", usuario.Id);
			cmd.ExecuteNonQuery();

			AdicionarParametrosDeAmizades(usuario, cmd);
		}

		public void Excluir(Guid id)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();
			
			using var cmd = new NpgsqlCommand(
				@"DELETE FROM public.amizade
					WHERE idusuario=@id;",
				con);
				
			cmd.Parameters.AddWithValue("id", id);
			cmd.ExecuteNonQuery();
			
			cmd.CommandText = 
				@"DELETE FROM public.usuario
					WHERE id=@id;";
			cmd.ExecuteNonQuery();
		}

		public void Inserir(Usuario usuario)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();
			
			using var cmd = new NpgsqlCommand(
				@"INSERT INTO public.usuario
					(id, nome, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo)
					VALUES(@id, @nome, @sobrenome, @senha, @datacomemorativa, @sexo, @bio, @fotodeperfil, @cidade, @uf, @telefone, @documento, @tipo);",
				con);
				
			AdicionarParametrosInserirOuAlterar(usuario, cmd);
			cmd.ExecuteNonQuery();
			
			AdicionarParametrosDeAmizades(usuario, cmd);
		}

		public Usuario Procurar(Guid id)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();
			
			using var cmd = new NpgsqlCommand(
				@"SELECT id, nome, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo
					FROM public.usuario WHERE id=@id;",
				con);
			
			cmd.Parameters.AddWithValue("id", id);
			
			List<Usuario> usuarios = [];
			using var reader = cmd.ExecuteReader();
			while(reader.Read())
				usuarios.Add(CriarUsuario(reader));
			reader.Close();
			
			AdicionarListaDeAmigos(con, usuarios);
			
			return usuarios.First();
		}

		public List<Usuario> ProcurarTodos()
        {
            using var con = new NpgsqlConnection(ConnectionString);
            con.Open();

            using var cmd = new NpgsqlCommand(
                @"SELECT id, nome, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo
					FROM public.usuario;",
                con);

            List<Usuario> usuarios = [];
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                usuarios.Add(CriarUsuario(reader));
            reader.Close();

            AdicionarListaDeAmigos(con, usuarios);

            return usuarios;
        }

        private void AdicionarListaDeAmigos(NpgsqlConnection con, List<Usuario> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                using var cmdAmizade = new NpgsqlCommand(
                    @"SELECT idusuario, idusuarioamigo
						FROM public.amizade WHERE idusuario=@idusuario;",
                    con);
                cmdAmizade.Parameters.AddWithValue("idusuario", usuario.Id);

                using var readerAmizade = cmdAmizade.ExecuteReader();
                while (readerAmizade.Read())
                    usuario.Amigos.Add(Procurar(readerAmizade.GetGuid(readerAmizade.GetOrdinal("idusuarioamigo"))));
                readerAmizade.Close();
            }
        }

        private void AdicionarParametrosInserirOuAlterar(Usuario usuario, NpgsqlCommand cmd)
		{
			cmd.Parameters.AddWithValue("id", usuario.Id);
			cmd.Parameters.AddWithValue("nome", usuario.Nome);
			cmd.Parameters.AddWithValue("sobrenome", usuario.Sobrenome);
			cmd.Parameters.AddWithValue("senha", usuario.Senha);
			cmd.Parameters.AddWithValue("datacomemorativa", usuario.DataComemorativa);
			cmd.Parameters.AddWithValue("sexo", (int)usuario.Sexo);
			cmd.Parameters.AddWithValue("bio", usuario.Bio);
			cmd.Parameters.AddWithValue("fotodeperfil", usuario.FotoPefil);
			cmd.Parameters.AddWithValue("cidade", usuario.Cidade);
			cmd.Parameters.AddWithValue("uf", (int)usuario.Uf);
			cmd.Parameters.AddWithValue("telefone", usuario.Telefone);
			cmd.Parameters.AddWithValue("documento", usuario.Documento);
			cmd.Parameters.AddWithValue("tipo", (int)usuario.Tipo);
		}
		
		private void AdicionarParametrosDeAmizades(Usuario usuario, NpgsqlCommand cmd)
		{
			foreach (var amizade in usuario.Amigos)
			{
				cmd.Parameters.Clear();
				cmd.CommandText = @"
					INSERT INTO public.amizade
						(idusuario, idusuarioamigo)
						VALUES(@idusuario, @idusuarioamigo);";

				cmd.Parameters.AddWithValue("idusuario", usuario.Id);
				cmd.Parameters.AddWithValue("idusuarioamigo", amizade.Id);
				cmd.ExecuteNonQuery();
			}
		}

		private Usuario CriarUsuario(NpgsqlDataReader reader) => new ()
			{
				Id = reader.GetGuid(reader.GetOrdinal("id")),
				Nome = reader.GetString(reader.GetOrdinal("nome")),
				Sobrenome = reader.GetString(reader.GetOrdinal("sobrenome")),
				Senha = reader.GetString(reader.GetOrdinal("senha")),
				DataComemorativa = reader.GetDateTime(reader.GetOrdinal("datacomemorativa")),
				Sexo = (TipoSexo)reader.GetInt32(reader.GetOrdinal("sexo")),
				Bio = reader.GetString(reader.GetOrdinal("bio")),
				Cidade = reader.GetString(reader.GetOrdinal("cidade")),
				Uf = (EstadosBrasil)reader.GetInt32(reader.GetOrdinal("uf")),
				Telefone = reader.GetString(reader.GetOrdinal("telefone")),
				Documento = reader.GetString(reader.GetOrdinal("documento")),
				Tipo = (TipoPessoa)reader.GetInt32(reader.GetOrdinal("tipo")),
			};
	}
}
