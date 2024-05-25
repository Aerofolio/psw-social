using SocialUniftec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Domain.Repository
{
    public interface INotificacaoRepository
    {
        void Inserir(Notificacao notificacao);
        void Alterar(Notificacao notificacao);
        void Excluir(Guid id);
        Notificacao Procurar(Guid id);
        List<Notificacao> ProcurarTodos();
    }
}
