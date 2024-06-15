namespace SocialUniftec.API.Models
{
    public class NotificacaoResumidaModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioOrigemId { get; set; }
        public Guid UsuarioDestinoId { get; set; }
        public TipoNotificacaoModel Tipo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime? DataLeitura { get; set; }
    }

}
