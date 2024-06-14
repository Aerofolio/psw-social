using SocialUniftec.API.Models;
using SocialUniftec.Application.Dto;

namespace SocialUniftec.API.Adapter
{
    public class UsuarioLoginMapping
    {

        public static UsuarioLoginModel? ToModel(UsuarioDto? usuario)
        {

            if (usuario == null)
            {
                return null;
            }

            UsuarioLoginModel usuarioLoginModel = new UsuarioLoginModel();
            usuarioLoginModel.Senha = usuario.Senha;
            usuarioLoginModel.Email = usuario.Email;
            
            return usuarioLoginModel;
        }

        public static UsuarioDto? ToDto(UsuarioLoginModel usuario)
        {

            if (usuario == null)
            {
                return null;
            }

            UsuarioDto usuarioDto = new UsuarioDto();
            usuarioDto.Email = usuario.Email;
            usuarioDto.Senha = usuario.Senha;

            return usuarioDto;

        }
    }
}