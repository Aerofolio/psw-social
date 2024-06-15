using Npgsql;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Domain.Repository;

namespace SocialUniftec.Repository.Repository
{
	public class UsuarioRepository : IUsuarioRepository
	{
        private string ConnectionString;

        public UsuarioRepository(string strConexao = null)
        {
            this.ConnectionString = strConexao;
        }

        public void Alterar(Usuario usuario)
		{
			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();
				
				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

                    cmd.Connection = con;
					cmd.CommandText = @"UPDATE public.usuario
									SET nome=@nome, email=@email, sobrenome=@sobrenome, senha=@senha, datacomemorativa=@datacomemorativa, sexo=@sexo, 
									bio=@bio, fotodeperfil=@fotodeperfil, cidade=@cidade, uf=@uf, telefone=@telefone, documento=@documento, tipo=@tipo
									WHERE id=@id;";

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
			}
		}

		public void Excluir(Guid id)
		{
			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"DELETE FROM public.amizade
						WHERE idusuario=@id;";


					cmd.Parameters.AddWithValue("id", id);
					cmd.ExecuteNonQuery();

					cmd.CommandText =
						@"DELETE FROM public.usuario
						WHERE id=@id;";
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void Inserir(Usuario usuario)
		{
			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"INSERT INTO public.usuario
					(id, email, nome, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo)
					VALUES(@id, @email, @nome, @sobrenome, @senha, @datacomemorativa, @sexo, @bio, @fotodeperfil, @cidade, @uf, @telefone, @documento, @tipo);";

					AdicionarParametrosInserirOuAlterar(usuario, cmd);
					cmd.ExecuteNonQuery();

					AdicionarParametrosDeAmizades(usuario, cmd);
				}
			}
		}

        public Usuario Procurar(Guid id)
        {
            using (var con = new NpgsqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"SELECT id, nome, email, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo
                                        FROM public.usuario WHERE id=@id;";
                    cmd.Parameters.AddWithValue("id", id);

                    Usuario usuario = null;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = CriarUsuario(reader);
                        }
                    }

                    if (usuario != null)
                    {
                        AdicionarAmigos(con, usuario);
                    }

                    return usuario;
                }
            }
        }
        private void AdicionarAmigos(NpgsqlConnection con, Usuario usuario)
        {
            using (var cmdAmizade = new NpgsqlCommand(
                @"SELECT u.id, u.nome, u.email, u.sobrenome, u.senha, u.datacomemorativa, u.sexo, u.bio, u.fotodeperfil, u.cidade, u.uf, u.telefone, u.documento, u.tipo
          FROM public.amizade a
          JOIN public.usuario u ON a.idusuarioamigo = u.id
          WHERE a.idusuario = @idusuario;", con))
            {
                cmdAmizade.Parameters.AddWithValue("idusuario", usuario.Id);

                using (var readerAmizade = cmdAmizade.ExecuteReader())
                {
                    while (readerAmizade.Read())
                    {
                        usuario.Amigos.Add(CriarUsuario(readerAmizade));
                    }
                }
            }
        }

        public void RemoverAmigo(Guid idUsuario, Guid idAmigo)
        {
            using (var con = new NpgsqlConnection(ConnectionString))
            {
                con.Open();

                using (var transaction = con.BeginTransaction())
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;
                    cmd.Transaction = transaction;

                    cmd.CommandText = @"
                DELETE FROM public.amizade
                WHERE (idusuario = @idUsuario AND idusuarioamigo = @idAmigo) OR
                      (idusuario = @idAmigo AND idusuarioamigo = @idUsuario);";

                    cmd.Parameters.AddWithValue("idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("idAmigo", idAmigo);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        public List<Usuario> ProcurarTodos()
        {
            using (var con = new NpgsqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"SELECT id, nome, email, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo
                                        FROM public.usuario;";

                    var usuarios = new List<Usuario>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(CriarUsuario(reader));
                        }
                    }

                    AdicionarListaDeAmigos(con, usuarios);

                    return usuarios;
                }
            }
        }

        private void AdicionarListaDeAmigos(NpgsqlConnection con, List<Usuario> usuarios)
        {
            var usuarioIds = usuarios.Select(u => u.Id).ToArray();

            using (var cmdAmizade = new NpgsqlCommand(
                @"SELECT idusuario, idusuarioamigo
                  FROM public.amizade WHERE idusuario = ANY(@ids);", con))
            {
                cmdAmizade.Parameters.AddWithValue("ids", usuarioIds);

                var amizades = new Dictionary<Guid, List<Guid>>();
                using (var readerAmizade = cmdAmizade.ExecuteReader())
                {
                    while (readerAmizade.Read())
                    {
                        var idUsuario = readerAmizade.GetGuid(readerAmizade.GetOrdinal("idusuario"));
                        var idAmigo = readerAmizade.GetGuid(readerAmizade.GetOrdinal("idusuarioamigo"));

                        if (!amizades.ContainsKey(idUsuario))
                        {
                            amizades[idUsuario] = new List<Guid>();
                        }

                        amizades[idUsuario].Add(idAmigo);
                    }
                }

                foreach (var usuario in usuarios)
                {
                    if (amizades.TryGetValue(usuario.Id, out var amigos))
                    {
                        usuario.Amigos.AddRange(amigos.Select(id => new Usuario { Id = id }));
                    }
                }
            }
        }

        private void AdicionarParametrosInserirOuAlterar(Usuario usuario, NpgsqlCommand cmd)
		{
			cmd.Parameters.AddWithValue("id", usuario.Id);
            cmd.Parameters.AddWithValue("email", usuario.Email);
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
			    Email = reader.GetString(reader.GetOrdinal("email")),
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

        public Usuario ProcurarPorEmailESenha(string email, string senha)
        {

			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"SELECT id, nome, email, sobrenome, senha, datacomemorativa, sexo, bio, fotodeperfil, cidade, uf, telefone, documento, tipo
					FROM public.usuario WHERE email=@email AND senha=@senha;";

					cmd.Parameters.AddWithValue("email", email);
					cmd.Parameters.AddWithValue("senha", senha);

					List<Usuario> usuarios = [];
					using var reader = cmd.ExecuteReader();
					while (reader.Read())
						usuarios.Add(CriarUsuario(reader));
					reader.Close();

					AdicionarListaDeAmigos(con, usuarios);

					return usuarios.First();
				}
			}
        }
    }
}
