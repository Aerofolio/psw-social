using Npgsql;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Repository.Repository
{
	public class NotificacaoRepository : INotificacaoRepository
	{
		private readonly string ConnectionString = "User ID=postgres;Password=;Host=localhost;Port=5432;Database=socialuniftec;";
		
		public void Alterar(Notificacao notificacao)
		{
			throw new NotImplementedException();
		}

		public void Excluir(Guid id)
		{
			throw new NotImplementedException();
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
				
			cmd.Parameters.AddWithValue("id", notificacao.Id);
			cmd.Parameters.AddWithValue("idusuarioorigem", notificacao.UsuarioOrigem.Id);
			cmd.Parameters.AddWithValue("idusuariodestino", notificacao.UsuarioDestino.Id);
			cmd.Parameters.AddWithValue("tipo", (int)notificacao.Tipo);
			cmd.Parameters.AddWithValue("mensagem", notificacao.Mensagem);
			cmd.Parameters.AddWithValue("dataenvio", notificacao.DataEnvio);
			cmd.Parameters.AddWithValue("dataleitura", notificacao.DataLeitura);
			
			cmd.ExecuteNonQuery();
		}

		public Notificacao Procurar(Guid id)
		{
			throw new NotImplementedException();
		}

		public List<Notificacao> ProcurarTodos()
		{
			throw new NotImplementedException();
		}
	}
}
