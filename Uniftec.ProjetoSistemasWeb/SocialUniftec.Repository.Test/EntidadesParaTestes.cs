using SocialUniftec.Domain.Entities;

namespace SocialUniftec.Repository.Test;

public static class EntidadesParaTestes
{
	public static readonly Usuario UsuarioTeste = new ()
		{
			Id = Guid.Parse("52a06685-f6a1-4109-a25e-be7d76e8bb0e"),
			Nome = "Carinha",
			Sobrenome = "Que Mora Logo Ali",
			Senha = "OdeioATonya",
			DataComemorativa = DateTime.Now,
			Sexo = TipoSexo.Masculino,
			Bio = "Brooklyn 1983",
			Cidade = "New York",
			Uf = EstadosBrasil.AC,
			Telefone = "91234-5678",
			Documento = "012345678-09",
			Tipo = TipoPessoa.Fisica
		};
		
	public static readonly Usuario UsuarioAmizade = new ()
		{
			Id = Guid.Parse("41469b6b-470f-425d-99af-f42b9b938460"),
			Nome = "Julius",
			Sobrenome = "Rock",
			Senha = "AmoARochelle",
			DataComemorativa = new DateTime(1932, 12, 28),
			Sexo = TipoSexo.Masculino,
			Bio = "Trabalho em dois empregos",
			Cidade = "New York",
			Uf = EstadosBrasil.AC,
			Telefone = "91324-5776",
			Documento = "012345687-49",
			Tipo = TipoPessoa.Fisica,
		};
		
	
	public static readonly Notificacao NotificacaoTeste = new()
		{
			Id = Guid.Parse("f9ba7740-95bd-495e-b4c6-b6dec229db9d"),
			UsuarioOrigem = UsuarioTeste,
			UsuarioDestino = UsuarioAmizade,
			Tipo = TipoNotificacao.Solicitacao_Amizade,
			Mensagem = $"{UsuarioTeste.Nome} deseja ser seu amigo!",
			DataEnvio = DateTime.Now,
			DataLeitura = DateTime.Now
		};
}