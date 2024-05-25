using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialUniftec.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                usuarioRepository.Inserir(new Domain.Entities.Usuario());
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
                usuarioRepository.Alterar(new Domain.Entities.Usuario());
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
