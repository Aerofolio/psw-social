using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialUniftec.Repository.Repository;

namespace SocialUniftec.Repository.Test
{
	[TestClass]
	public class UsuarioRepositoryTest
	{	
		[TestMethod]
		public void InserirTest()
		{
			var usuarioRepository = new UsuarioRepository();
			try
			{
				usuarioRepository.Inserir(EntidadesParaTestes.UsuarioAmizade);
				EntidadesParaTestes.UsuarioTeste.AdicionarAmigo(EntidadesParaTestes.UsuarioAmizade);
				usuarioRepository.Inserir(EntidadesParaTestes.UsuarioTeste);
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
			var usuarioRepository = new UsuarioRepository();
			try
			{
				var usuario = EntidadesParaTestes.UsuarioTeste;
				usuario.DataComemorativa = new DateTime(1965, 2, 7);
				EntidadesParaTestes.UsuarioTeste.AdicionarAmigo(EntidadesParaTestes.UsuarioAmizade);
				
				usuarioRepository.Alterar(usuario);
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
			var usuarioRepository = new UsuarioRepository();
			try
			{
				usuarioRepository.Excluir(EntidadesParaTestes.UsuarioTeste.Id);
				usuarioRepository.Excluir(EntidadesParaTestes.UsuarioAmizade.Id);
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
			var usuarioRepository = new UsuarioRepository();
			try
			{
				var usuario = usuarioRepository.Procurar(EntidadesParaTestes.UsuarioTeste.Id);
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
			var usuarioRepository = new UsuarioRepository();
			try
			{
				var usuario = usuarioRepository.ProcurarTodos();
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

	}
}
