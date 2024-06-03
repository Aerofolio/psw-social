using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				var clientes = notificacaoRepository.ProcurarTodos();
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
