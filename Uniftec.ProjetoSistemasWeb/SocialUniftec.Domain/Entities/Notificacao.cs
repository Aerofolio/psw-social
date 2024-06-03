using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Domain.Entities
{
    public class Notificacao
    {
        public Guid Id { get; set; }
        public Usuario UsuarioOrigem { get; set; }
        public Usuario UsuarioDestino { get; set;}
        public TipoNotificacao Tipo {  get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime? DataLeitura { get; set; }

        public Notificacao()
        {
            this.Id = Guid.NewGuid();
            this.UsuarioOrigem = new Usuario();
            this.UsuarioDestino = new Usuario();
            this.Tipo = TipoNotificacao.Solicitacao_Amizade;
            this.Mensagem = string.Empty;
            this.DataEnvio = DateTime.Now;
        }

    }
    
    public enum TipoNotificacao
    {
        Solicitacao_Amizade = 0,
    }
}
