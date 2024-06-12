using SocialUniftec.API.Models;
using SocialUniftec.Application.Dto;
using SocialUniftec.Domain.Entities;

namespace SocialUniftec.Application.Adapter
{
    public static class NotificacaoMapping
    {
        public static NotificacaoModel? ToModel(NotificacaoDto? notificacao)
        {

            if (notificacao == null)
            {
                return null;
            }

            NotificacaoModel notificacaoModel = new NotificacaoModel();
            notificacaoModel.Id = notificacao.Id;
            notificacaoModel.UsuarioOrigem = UsuarioMapping.ToModel(notificacao.UsuarioOrigem);
            notificacaoModel.UsuarioDestino = UsuarioMapping.ToModel(notificacao.UsuarioDestino);
            notificacaoModel.Tipo = (TipoNotificacaoModel) notificacao.Tipo;
            notificacaoModel.Mensagem = notificacao.Mensagem;
            notificacaoModel.DataEnvio = notificacao.DataEnvio;
            notificacaoModel.DataLeitura = notificacao.DataLeitura;

            return notificacaoModel;
        }

        public static NotificacaoDto? ToDto(NotificacaoModel notificacao)
        {

            if (notificacao == null)
            {
                return null;
            }

            NotificacaoDto notificacaoDto = new NotificacaoDto();
            notificacaoDto.Id = notificacao.Id;
            notificacaoDto.UsuarioOrigem = UsuarioMapping.ToDto(notificacao.UsuarioOrigem);
            notificacaoDto.UsuarioDestino = UsuarioMapping.ToDto(notificacao.UsuarioDestino);
            notificacaoDto.Tipo = (TipoNotificacaoDto) notificacao.Tipo;
            notificacaoDto.Mensagem = notificacao.Mensagem;
            notificacaoDto.DataEnvio = notificacao.DataEnvio;
            notificacaoDto.DataLeitura = notificacao.DataLeitura;

            return notificacaoDto;

        }

    }
}
