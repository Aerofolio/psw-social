﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                notificacaoRepository.Inserir(new Domain.Entities.Notificacao());
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
                notificacaoRepository.Alterar(new Domain.Entities.Notificacao());
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
                notificacaoRepository.Excluir(Guid.NewGuid());
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
                var cliente = notificacaoRepository.Procurar(Guid.NewGuid());
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