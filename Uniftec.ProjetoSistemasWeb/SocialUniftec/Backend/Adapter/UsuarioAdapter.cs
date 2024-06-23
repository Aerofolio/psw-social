using SocialUniftec.Models;
using SocialUniftec.Website.Backend;

namespace SocialUniftec.Website.Backend.Adapter
{
    public static class UsuarioAdapter
    {
        public static UsuarioModel ToModel(UsuarioCadastroModel usuarioCadastro)
        {
            return new ()
            {
                Nome = usuarioCadastro.Nome,
                Sobrenome = usuarioCadastro.Sobrenome,
                Email = usuarioCadastro.Email,
                Senha = usuarioCadastro.Senha!,
                DataComemorativa = usuarioCadastro.DataComemorativa,
                Sexo = TipoSexoModel.Masculino,
                Bio = "teste",
                FotoPerfil = string.Empty,
                Cidade = "teste",
                Uf = EstadosBrasilModel.AC,
                Telefone = "teste",
                Documento = "teste",
                Tipo = TipoPessoaModel.Juridica,
                Amigos = []   
            };
        }
    } 
}