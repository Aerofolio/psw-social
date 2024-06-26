﻿using System.ComponentModel.DataAnnotations;
using SocialUniftec.Website.Backend;

namespace SocialUniftec.Models
{
    public class UsuarioAlterarModel : UsuarioCadastroModel
    {
        public Guid Id { get; set; }
        public string Telefone { get; set; }
        public TipoPessoaModel TipoPessoa { get; set; }
        public TipoSexoModel TipoSexo { get; set; }
        public string Documento { get; set; }
        public string Bio { get; set; }
    }
}
