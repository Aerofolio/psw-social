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
    public class NotificacaoApplication
    {

        NotificacaoRepository notificacaoRepository;

        public NotificacaoApplication()
        {
            string strConexao = "User ID=postgres;Password=123456789;Host=localhost;Port=5432;Database=socialuniftec;";
            this.notificacaoRepository = new NotificacaoRepository(strConexao);
        }


        public Guid Inserir(NotificacaoDto notificacao)
        {

            Notificacao notif = NotificacaoAdapter.ToDomain(notificacao)!;
            notif.Id = Guid.NewGuid();

            notificacaoRepository.Inserir(notif);
            return notif.Id;
        }

        public Guid Alterar(NotificacaoDto notificacao)
        {

            Notificacao notif = NotificacaoAdapter.ToDomain(notificacao)!;

            notificacaoRepository.Alterar(notif);

            return notif.Id;
        }

        public void Excluir(Guid id)
        {
            notificacaoRepository.Excluir(id);
        }

        public NotificacaoDto Procurar(Guid id)
        {

            Notificacao notificacao = notificacaoRepository.Procurar(id);

            return NotificacaoAdapter.ToDto(notificacao)!;
        }

        public List<NotificacaoDto> ProcurarTodos()
        {

            List<Notificacao> notificacaos = notificacaoRepository.ProcurarTodos();

            List<NotificacaoDto> notificacaosDto = [];

            foreach (Notificacao notificacao in notificacaos)
            {
                notificacaosDto.Add(NotificacaoAdapter.ToDto(notificacao)!);
            }

            return notificacaosDto;
        }

        public Guid Ler(Guid id)
        {
            Notificacao notificacao = notificacaoRepository.Procurar(id);
            notificacao.DataLeitura = DateTime.Now;

            notificacaoRepository.Alterar(notificacao);

            return notificacao.Id;
        }

        public List<NotificacaoDto> ProcurarNotificacoesPendentes(Guid idUsuario)
        {

            List<Notificacao> notificacaos = notificacaoRepository.ProcurarNotificacoesPendentes(idUsuario);

            List<NotificacaoDto> notificacaosDto = [];

            foreach (Notificacao notificacao in notificacaos)
            {
                notificacaosDto.Add(NotificacaoAdapter.ToDto(notificacao)!);
            }

            return notificacaosDto;
        }
    }
}
