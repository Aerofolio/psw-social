using Npgsql;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Domain.Repository;

namespace SocialUniftec.Repository.Repository
{
	public class NotificacaoRepository : INotificacaoRepository
	{
        private string ConnectionString;

        public NotificacaoRepository(string ConnectionString = null)
        {
            this.ConnectionString = ConnectionString;
        }
		
		public void Alterar(Notificacao notificacao)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();

			using var cmd = new NpgsqlCommand(
				@"UPDATE public.notificacao
					SET idusuarioorigem=@idusuarioorigem, idusuariodestino=@idusuariodestino, tipo=@tipo, mensagem=@mensagem, dataenvio=@dataenvio, dataleitura=@dataleitura
					WHERE id=@id;",
				con);
				
			AdicionarParametrosInserirOuAlterar(notificacao, cmd);
			
			cmd.ExecuteNonQuery();
		}

		public void Excluir(Guid id)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();
			
			using var cmd = new NpgsqlCommand(
				@"DELETE FROM public.notificacao
					WHERE id=@id;",
				con);
				
			cmd.Parameters.AddWithValue("id", id);
			cmd.ExecuteNonQuery();
		}

		public void Inserir(Notificacao notificacao)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();

			using var cmd = new NpgsqlCommand(
				@"INSERT INTO public.notificacao
					(id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura)
					VALUES(@id, @idusuarioorigem, @idusuariodestino, @tipo, @mensagem, @dataenvio, @dataleitura);",
				con);

			AdicionarParametrosInserirOuAlterar(notificacao, cmd);

			cmd.ExecuteNonQuery();
		}

		public Notificacao Procurar(Guid id)
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();

			using var cmd = new NpgsqlCommand(
				@"SELECT id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura
					FROM public.notificacao WHERE id=@id;",
				con);
				
			cmd.Parameters.AddWithValue("id", id);
			
			List<Notificacao> notificacaos = [];
			using var reader = cmd.ExecuteReader();
			while(reader.Read())
				notificacaos.Add(CriarNotificacao(reader));
			reader.Close();
			
			
			return notificacaos.First();
		}

        public List<Notificacao> ProcurarNotificacoesPendentes(Guid idUsuario)
        {
            using var con = new NpgsqlConnection(ConnectionString);
          
			con.Open();

            using var cmd = new NpgsqlCommand(@"SELECT id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura FROM public.notificacao WHERE idusuarioorigem = @idusuarioorigem AND dataleitura IS NULL;", con);

			cmd.Parameters.AddWithValue("idusuarioorigem", idUsuario);

            List<Notificacao> notificacaos = [];
           
			using var reader = cmd.ExecuteReader();
           
			while (reader.Read())
                notificacaos.Add(CriarNotificacao(reader));

            reader.Close();

            return notificacaos;

        }

        public List<Notificacao> ProcurarTodos()
		{
			using var con = new NpgsqlConnection(ConnectionString);
			con.Open();

			using var cmd = new NpgsqlCommand(
				@"SELECT id, idusuarioorigem, idusuariodestino, tipo, mensagem, dataenvio, dataleitura
					FROM public.notificacao;",
				con);
			
			List<Notificacao> notificacaos = [];
			using var reader = cmd.ExecuteReader();
			while(reader.Read())
				notificacaos.Add(CriarNotificacao(reader));
				
			reader.Close();
			
			return notificacaos;
		}
		
		private void AdicionarParametrosInserirOuAlterar(Notificacao notificacao, NpgsqlCommand cmd)
		{
			cmd.Parameters.AddWithValue("id", notificacao.Id);
			cmd.Parameters.AddWithValue("idusuarioorigem", notificacao.UsuarioOrigem.Id);
			cmd.Parameters.AddWithValue("idusuariodestino", notificacao.UsuarioDestino.Id);
			cmd.Parameters.AddWithValue("tipo", (int)notificacao.Tipo);
			cmd.Parameters.AddWithValue("mensagem", notificacao.Mensagem);
			cmd.Parameters.AddWithValue("dataenvio", notificacao.DataEnvio);
			cmd.Parameters.AddWithValue("dataleitura", notificacao.DataLeitura);
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
