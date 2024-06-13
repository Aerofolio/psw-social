using Microsoft.AspNetCore.Mvc;
using SocialUniftec.API.Models;
using SocialUniftec.Application.Adapter;
using SocialUniftec.Application.Application;
using SocialUniftec.Application.Dto;

namespace SocialUniftec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController
    {

        [HttpPost]
        public Guid Post(NotificacaoModel notificacao)
        {
            NotificacaoApplication application = new NotificacaoApplication();
            return application.Inserir(NotificacaoMapping.ToDto(notificacao));
        }

        [HttpPut]
        public Guid Put(NotificacaoModel notificacao)
        {
            NotificacaoApplication application = new NotificacaoApplication();
            return application.Alterar(NotificacaoMapping.ToDto(notificacao));
        }

        [HttpDelete("{id:Guid}")]
        public void Delete(Guid id)
        {
            NotificacaoApplication notificacaoApplication = new NotificacaoApplication();
            notificacaoApplication.Excluir(id);
        }

        [HttpGet("{id:Guid}")]
        public NotificacaoModel Get(Guid id)
        {
            NotificacaoApplication notificacaoApplication = new NotificacaoApplication();
            NotificacaoDto notificacao = notificacaoApplication.Procurar(id);
            return NotificacaoMapping.ToModel(notificacao);
        }

        [HttpPut("{id:Guid}/Ler")]
        public Guid Ler(Guid id)
        {
            NotificacaoApplication application = new NotificacaoApplication();
            return application.Ler(id);
        }

        [HttpGet("{idUsuario:Guid}/Pendentes")]
        public List<NotificacaoModel> ProcurarNotificacoesPendentes(Guid idUsuario)
        {
            NotificacaoApplication notificacaoApplication = new NotificacaoApplication();
            
            List<NotificacaoDto> notificacoesDto = notificacaoApplication.ProcurarNotificacoesPendentes(idUsuario);

            List<NotificacaoModel> notificacoes = [];

            foreach (var notificacao in notificacoesDto)
            {
                notificacoes.Add(NotificacaoMapping.ToModel(notificacao));
            }

            return notificacoes;
        }

    }
}
