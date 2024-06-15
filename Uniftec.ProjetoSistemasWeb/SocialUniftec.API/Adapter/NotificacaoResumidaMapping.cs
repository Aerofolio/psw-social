using SocialUniftec.API.Models;
using SocialUniftec.Application.Adapter;
using SocialUniftec.Application.Dto;

namespace SocialUniftec.API.Adapter
{
    public class NotificacaoResumidaMapping
    {
        public static NotificacaoResumidaModel? ToModel(NotificacaoDto? notificacao)
        {

            if (notificacao == null)
            {
                return null;
            }

            NotificacaoResumidaModel notificacaoModel = new NotificacaoResumidaModel();
            notificacaoModel.Id = notificacao.Id;
            notificacaoModel.UsuarioOrigemId = UsuarioMapping.ToModel(notificacao.UsuarioOrigem).Id;
            notificacaoModel.UsuarioDestinoId = UsuarioMapping.ToModel(notificacao.UsuarioDestino).Id;
            notificacaoModel.Tipo = (TipoNotificacaoModel)notificacao.Tipo;
            notificacaoModel.Mensagem = notificacao.Mensagem;
            notificacaoModel.DataEnvio = notificacao.DataEnvio;
            notificacaoModel.DataLeitura = notificacao.DataLeitura;

            return notificacaoModel;
        }

        public static NotificacaoDto? ToDto(NotificacaoResumidaModel notificacao)
        {

            if (notificacao == null)
            {
                return null;
            }

            NotificacaoDto notificacaoDto = new NotificacaoDto();
            notificacaoDto.Id = notificacao.Id;
            notificacaoDto.UsuarioOrigem = new UsuarioDto()
            {
                Id = notificacao.UsuarioOrigemId,
                Amigos = [],
            };
            notificacaoDto.UsuarioDestino = new UsuarioDto()
            {
                Id = notificacao.UsuarioDestinoId,
                Amigos = [],
            };
            notificacaoDto.Tipo = (TipoNotificacaoDto)notificacao.Tipo;
            notificacaoDto.Mensagem = notificacao.Mensagem;
            notificacaoDto.DataEnvio = notificacao.DataEnvio;
            notificacaoDto.DataLeitura = notificacao.DataLeitura;

            return notificacaoDto;

        }

    }
}
