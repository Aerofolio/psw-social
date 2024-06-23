using SocialUniftec.Models;

namespace SocialUniftec.Website.Backend.Adapter
{
    public static class UsuarioAdapter
    {
        public static UsuarioModel ToUsuarioModel(UsuarioCadastroModel usuarioCadastro)
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
        
        public static UsuarioCadastroModel ToUsuarioCadastroModel(UsuarioModel usuarioModel)
        {
            return new ()
            {
                Nome = usuarioModel.Nome,
                Sobrenome = usuarioModel.Sobrenome,
                Email = usuarioModel.Email,
                DataComemorativa = usuarioModel.DataComemorativa
            };
        }
        
        public static UsuarioLoginModel ToUsuarioLoginModel(LoginModel login)
        {
            return new UsuarioLoginModel()
            {
                Email = login.Email,
                Senha = login.Senha,
            };
        }
    } 
}