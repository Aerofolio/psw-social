using SocialUniftec.Domain.Entities;

namespace SocialUniftec.API.Models
{
    public class NotificacaoModel
    {
        public Guid Id { get; set; }
        public UsuarioModel UsuarioOrigem { get; set; }
        public UsuarioModel UsuarioDestino { get; set; }
        public TipoNotificacaoModel Tipo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime? DataLeitura { get; set; }

    }

    public enum TipoNotificacaoModel
    {
        Solicitacao_Amizade = 0,
    }
}
