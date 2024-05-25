using SocialUniftec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Domain.Repository
{
    public interface IUsuarioRepository
    {
        void Inserir(Usuario usuario);
        void Alterar(Usuario usuario);
        void Excluir(Guid id);
        Usuario Procurar(Guid id);
        List<Usuario> ProcurarTodos();

    }
}
