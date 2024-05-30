using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialUniftec.Domain.Entities;
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
				usuarioRepository.Inserir(new Usuario()
				{
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
					Tipo = TipoPessoa.Fisica,
				});
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
				usuarioRepository.Alterar(new Usuario());
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
				usuarioRepository.Excluir(Guid.NewGuid());
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
				var cliente = usuarioRepository.Procurar(Guid.NewGuid());
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
				var clientes = usuarioRepository.ProcurarTodos();
				Assert.IsTrue(true);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

	}
}
