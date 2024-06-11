using SocialUniftec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialUniftec.Application.Dto
{
    public class NotificacaoDto
    {
        public Guid Id { get; set; }
        public UsuarioDto UsuarioOrigem { get; set; }
        public UsuarioDto UsuarioDestino { get; set; }
        public TipoNotificacaoDto Tipo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime? DataLeitura { get; set; }

    }
    public enum TipoNotificacaoDto
    {
        Solicitacao_Amizade = 0,
    }
}
