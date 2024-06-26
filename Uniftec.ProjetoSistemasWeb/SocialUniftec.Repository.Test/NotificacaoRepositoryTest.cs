﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialUniftec.Repository.Repository;

namespace SocialUniftec.Repository.Test
{
	[TestClass]
	public class NotificacaoRepositoryTest
	{
		[TestMethod]
		public void InserirTest()
		{
			var notificacaoRepository = new NotificacaoRepository();
			try
			{
				notificacaoRepository.Inserir(EntidadesParaTestes.NotificacaoTeste);
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void AlterarTest()
		{
			var notificacaoRepository = new NotificacaoRepository();
			try
			{
				EntidadesParaTestes.NotificacaoTeste.Mensagem = $"{EntidadesParaTestes.NotificacaoTeste.UsuarioOrigem.Nome} {EntidadesParaTestes.NotificacaoTeste.UsuarioOrigem.Sobrenome} deseja ser seu amigo!";
				notificacaoRepository.Alterar(EntidadesParaTestes.NotificacaoTeste);
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ExcluirTest()
		{
			var notificacaoRepository = new NotificacaoRepository();
			try
			{
				notificacaoRepository.Excluir(EntidadesParaTestes.NotificacaoTeste.Id);
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ProcurarTest()
		{
			var notificacaoRepository = new NotificacaoRepository();
			try
			{
				var notificacao = notificacaoRepository.Procurar(EntidadesParaTestes.NotificacaoTeste.Id);
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ProcurarTodosTest()
		{
			var notificacaoRepository = new NotificacaoRepository();
			try
			{
				var notificacaos = notificacaoRepository.ProcurarTodos();
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
