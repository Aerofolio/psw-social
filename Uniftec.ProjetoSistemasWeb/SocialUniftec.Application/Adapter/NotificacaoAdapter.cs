using SocialUniftec.Application.Dto;
using SocialUniftec.Domain.Entities;

namespace SocialUniftec.Application.Adapter
{
    public static class NotificacaoAdapter
    {
        public static Notificacao? ToDomain(NotificacaoDto? notificacao)
        {

            if (notificacao == null)
            {
                return null;
            }

            Notificacao notificacaoDomain = new Notificacao();
            notificacaoDomain.Id = notificacao.Id;
            notificacaoDomain.UsuarioOrigem = UsuarioAdapter.ToDomain(notificacao.UsuarioOrigem);
            notificacaoDomain.UsuarioDestino = UsuarioAdapter.ToDomain(notificacao.UsuarioDestino);
            notificacaoDomain.Tipo = (TipoNotificacao) notificacao.Tipo;
            notificacaoDomain.Mensagem = notificacao.Mensagem;
            notificacaoDomain.DataEnvio = notificacao.DataEnvio;
            notificacaoDomain.DataLeitura = notificacao.DataLeitura;

            return notificacaoDomain;
        }

        public static NotificacaoDto? ToDto(Notificacao notificacao)
        {

            if (notificacao == null)
            {
                return null;
            }

            NotificacaoDto notificacaoDto = new NotificacaoDto();
            notificacaoDto.Id = notificacao.Id;
            notificacaoDto.UsuarioOrigem = UsuarioAdapter.ToDto(notificacao.UsuarioOrigem);
            notificacaoDto.UsuarioDestino = UsuarioAdapter.ToDto(notificacao.UsuarioDestino);
            notificacaoDto.Tipo = (TipoNotificacaoDto) notificacao.Tipo;
            notificacaoDto.Mensagem = notificacao.Mensagem;
            notificacaoDto.DataEnvio = notificacao.DataEnvio;
            notificacaoDto.DataLeitura = notificacao.DataLeitura;

            return notificacaoDto;

        }

    }
}
