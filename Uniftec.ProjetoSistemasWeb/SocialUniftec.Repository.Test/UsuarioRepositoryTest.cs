using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Repository.Repository;

namespace SocialUniftec.Repository.Test
{
	[TestClass]
	public class UsuarioRepositoryTest
	{
		private readonly Usuario UsuarioTeste = new ()
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
		
		private readonly Usuario UsuarioAmizade = new ()
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
		
		[TestMethod]
		public void InserirTest()
		{
			var usuarioRepository = new UsuarioRepository();
			try
			{
				usuarioRepository.Inserir(UsuarioAmizade);
				UsuarioTeste.AdicionarAmigo(UsuarioAmizade);
				usuarioRepository.Inserir(UsuarioTeste);
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
				var usuario = UsuarioTeste;
				usuario.DataComemorativa = new DateTime(1965, 2, 7);
				UsuarioTeste.AdicionarAmigo(UsuarioAmizade);
				
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
				usuarioRepository.Excluir(UsuarioTeste.Id);
				usuarioRepository.Excluir(UsuarioAmizade.Id);
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
				var cliente = usuarioRepository.Procurar(UsuarioTeste.Id);
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
