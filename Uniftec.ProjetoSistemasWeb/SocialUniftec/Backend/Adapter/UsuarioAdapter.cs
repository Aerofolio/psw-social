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
                Bio = string.Empty,
                FotoPerfil = string.Empty,
                Cidade = string.Empty,
                Uf = EstadosBrasilModel.AC,
                Telefone = string.Empty,
                Documento = string.Empty,
                Tipo = TipoPessoaModel.Fisica,
                Amigos = []   
            };
        }
        
        public static UsuarioModel ToUsuarioModel(UsuarioAlterarModel usuarioAlterarModel)
        {
            return new ()
            {
                Id = usuarioAlterarModel.Id,
                Nome = usuarioAlterarModel.Nome,
                Sobrenome = usuarioAlterarModel.Sobrenome,
                Email = usuarioAlterarModel.Email,
                Senha = usuarioAlterarModel.Senha!,
                DataComemorativa = usuarioAlterarModel.DataComemorativa,
                Sexo = usuarioAlterarModel.TipoSexo,
                Bio = usuarioAlterarModel.Bio,
                FotoPerfil = string.Empty,//n temos como alterar
                Cidade = string.Empty,//n temos como alterar
                Uf = EstadosBrasilModel.AC,//n temos como alterar
                Telefone = usuarioAlterarModel.Telefone,
                Documento = usuarioAlterarModel.Documento,
                Tipo = usuarioAlterarModel.TipoPessoa,
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