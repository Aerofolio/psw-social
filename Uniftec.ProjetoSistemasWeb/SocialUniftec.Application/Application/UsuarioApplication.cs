using SocialUniftec.Application.Adapter;
using SocialUniftec.Application.Dto;
using SocialUniftec.Domain.Entities;
using SocialUniftec.Domain.Repository;
using SocialUniftec.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Application.Application
{
    public class UsuarioApplication
    {

        UsuarioRepository usuarioRepository;
        NotificacaoRepository notificacaoRepository;

        public UsuarioApplication()
        {
            string strConexao = "User ID=jmenzen2;Password='8N9;FLC?;@?I';Host=pgsql.jmenzen.com.br;Port=5432;Database=jmenzen2;";
            this.usuarioRepository = new UsuarioRepository(strConexao);
            this.notificacaoRepository = new NotificacaoRepository(strConexao);
        }

        public Guid Inserir(UsuarioDto usuario)
        {

            Usuario user = UsuarioAdapter.ToDomain(usuario)!;
            user.Id = Guid.NewGuid();

            usuarioRepository.Inserir(user);

            return user.Id;
        }

        public Guid Alterar(UsuarioDto usuario)
        {

            Usuario user = UsuarioAdapter.ToDomain(usuario)!;

            usuarioRepository.Alterar(user);

            return user.Id;
        }

        public void Excluir(Guid id)
        {
            usuarioRepository.Excluir(id);
        }

        public UsuarioDto Procurar(Guid id)
        {

            Usuario usuario = usuarioRepository.Procurar(id);

            return UsuarioAdapter.ToDto(usuario)!;
        }

        public List<UsuarioDto> ProcurarTodos()
        {

            List<Usuario> usuarios = usuarioRepository.ProcurarTodos();

            List<UsuarioDto> usuariosDto = [];

            foreach (Usuario usuario in usuarios)
            {
                usuariosDto.Add(UsuarioAdapter.ToDto(usuario)!);
            }

            return usuariosDto;
        }

        public Guid EnviarSolicitacaoAmizade(Guid id, Guid idUsuarioDestino)
        {

            Usuario usuarioOrigem = usuarioRepository.Procurar(id);

            Usuario usuarioDestino = new Usuario()
            {
                Id = idUsuarioDestino,
            };

            Notificacao notificacao = new Notificacao();
            notificacao.Tipo = TipoNotificacao.Solicitacao_Amizade;
            notificacao.Mensagem = usuarioOrigem.Nome + " " + usuarioOrigem.Sobrenome + " quer ser seu amigo!";
            notificacao.DataEnvio = DateTime.Now;
            notificacao.UsuarioOrigem = usuarioOrigem;
            notificacao.UsuarioDestino = usuarioDestino;

            notificacaoRepository.Inserir(notificacao);

            return notificacao.Id;
        }

        public UsuarioDto Login(UsuarioDto usuarioDto)
        {

            Usuario usuario = usuarioRepository.ProcurarPorEmailESenha(usuarioDto.Email, usuarioDto.Senha);

            return UsuarioAdapter.ToDto(usuario)!;
        }

        public void AceitarSolicitacaoAmizade(Guid id, Guid idUsuarioAmigo)
        {

            Usuario usuarioOrigem = usuarioRepository.Procurar(id);
            Usuario usuarioDestino = usuarioRepository.Procurar(idUsuarioAmigo);

            usuarioOrigem.AdicionarAmigo(usuarioDestino);
            usuarioDestino.AdicionarAmigo(usuarioOrigem);

            usuarioRepository.Alterar(usuarioOrigem);
            usuarioRepository.Alterar(usuarioDestino);

        }

        public void RemoverAmizade(Guid id, Guid idUsuarioAmigo)
        {
            usuarioRepository.RemoverAmigo(id, idUsuarioAmigo);
        }

        public List<UsuarioDto> ProcurarPorParametros(string nome, string sobrenome)
        {
            var usuarios = ProcurarTodos();

            if (!string.IsNullOrEmpty(nome))
            {
                usuarios = usuarios.Where(u => u.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(sobrenome))
            {
                usuarios = usuarios.Where(u => u.Sobrenome.Contains(sobrenome, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return usuarios;
        }
    }
}
