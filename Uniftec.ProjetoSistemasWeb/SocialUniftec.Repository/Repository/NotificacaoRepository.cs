using Npgsql;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Domain.Repository;

namespace SocialUniftec.Repository.Repository
{
	public class NotificacaoRepository : INotificacaoRepository
	{
        private string ConnectionString;

        public NotificacaoRepository(string ConnectionString)
        {
			if (ConnectionString == null)
			{
                ConnectionString = "User ID=postgres;Password=123456789;Host=localhost;Port=5432;Database=socialuniftec;"; ;

            }
            this.ConnectionString = ConnectionString;
        }
		
		public void Alterar(Notificacao notificacao)
		{

			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"UPDATE public.notificacao
					SET idusuarioorigem=@idusuarioorigem, idusuariodestino=@idusuariodestino, tipo=@tipo, mensagem=@mensagem, dataenvio=@dataenvio, dataleitura=@dataleitura
					WHERE id=@id;";

					AdicionarParametrosInserirOuAlterar(notificacao, cmd);

					cmd.ExecuteNonQuery();
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
					cmd.CommandText = @"DELETE FROM public.notificacao
					WHERE id=@id;";

					cmd.Parameters.AddWithValue("id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void Inserir(Notificacao notificacao)
		{
			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"INSERT INTO public.notificacao
					(id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura)
					VALUES(@id, @idusuarioorigem, @idusuariodestino, @tipo, @mensagem, @dataenvio, @dataleitura);";

					AdicionarParametrosInserirOuAlterar(notificacao, cmd);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public Notificacao Procurar(Guid id)
		{
			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"SELECT id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura
					FROM public.notificacao WHERE id=@id;";

					cmd.Parameters.AddWithValue("id", id);

					List<Notificacao> notificacaos = [];
					using var reader = cmd.ExecuteReader();
					while (reader.Read())
						notificacaos.Add(CriarNotificacao(reader));
					reader.Close();


					return notificacaos.First();
				}
			}
		}

        public List<Notificacao> ProcurarNotificacoesPendentes(Guid idUsuario)
        {
			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"SELECT id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura FROM public.notificacao WHERE idusuariodestino = @idusuariodestino AND dataleitura IS NULL;";

					cmd.Parameters.AddWithValue("idusuariodestino", idUsuario);

					List<Notificacao> notificacaos = [];

					using var reader = cmd.ExecuteReader();

					while (reader.Read())
						notificacaos.Add(CriarNotificacao(reader));

					reader.Close();

					return notificacaos;
				}
			}

        }

        public List<Notificacao> ProcurarTodos()
		{
			using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
			{
				con.Open();

				using (NpgsqlCommand cmd = new NpgsqlCommand())
				{

					cmd.Connection = con;
					cmd.CommandText = @"SELECT id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura
					FROM public.notificacao;";

					List<Notificacao> notificacaos = [];
					using var reader = cmd.ExecuteReader();
					while (reader.Read())
						notificacaos.Add(CriarNotificacao(reader));

					reader.Close();

					return notificacaos;
				}
			}
		}
		
		private void AdicionarParametrosInserirOuAlterar(Notificacao notificacao, NpgsqlCommand cmd)
		{
			cmd.Parameters.AddWithValue("id", notificacao.Id);
			cmd.Parameters.AddWithValue("idusuarioorigem", notificacao.UsuarioOrigem.Id);
			cmd.Parameters.AddWithValue("idusuariodestino", notificacao.UsuarioDestino.Id);
			cmd.Parameters.AddWithValue("tipo", (int)notificacao.Tipo);
			cmd.Parameters.AddWithValue("mensagem", notificacao.Mensagem);
			cmd.Parameters.AddWithValue("dataenvio", notificacao.DataEnvio);

			if (notificacao.DataLeitura != null)
			{
                cmd.Parameters.AddWithValue("dataleitura", notificacao.DataLeitura);
            } else
			{
                cmd.Parameters.AddWithValue("dataleitura", DBNull.Value);
            }

			
		}
		
		private Notificacao CriarNotificacao(NpgsqlDataReader reader)
		{
			var usuarioRepository= new UsuarioRepository();
			var usuarioOrigem = usuarioRepository.Procurar(reader.GetGuid(reader.GetOrdinal("idusuarioorigem")));
			var usuarioDestino = usuarioRepository.Procurar(reader.GetGuid(reader.GetOrdinal("idusuariodestino")));
			
			return new()
			{
				Id = reader.GetGuid(reader.GetOrdinal("id")),
				UsuarioOrigem = usuarioOrigem,
				UsuarioDestino = usuarioDestino,
				Tipo = (TipoNotificacao)reader.GetInt32(reader.GetOrdinal("tipo")),
				Mensagem = reader.GetString(reader.GetOrdinal("mensagem")),
				DataEnvio = reader.GetDateTime(reader.GetOrdinal("dataenvio")),
				DataLeitura = reader.GetDateTime(reader.GetOrdinal("dataleitura"))
			};
		}
	}
}
