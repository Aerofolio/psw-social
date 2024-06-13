﻿using SocialUniftec.Application.Adapter;
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
            string strConexao = "User ID=postgres;Password=123456789;Host=localhost;Port=5432;Database=socialuniftec;"; ;
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
    }
}