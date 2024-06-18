using System.Text;
using SocialUniftec.Application.Dto;
using SocialUniftec.Domain.Entities;

namespace SocialUniftec.Application.Adapter
{
    public class UsuarioAdapter
    {

        public static Usuario? ToDomain(UsuarioDto? usuario)
        {

            if (usuario == null)
            {
                return null;
            }

            Usuario usuarioDomain = new()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Senha = usuario.Senha,
                DataComemorativa = usuario.DataComemorativa,
                Sexo = (TipoSexo)usuario.Sexo,
                Bio = usuario.Bio,
                FotoPerfil = Convert.FromBase64String(usuario.FotoPerfil),
                Cidade = usuario.Cidade,
                Uf = (EstadosBrasil)usuario.Uf,
                Telefone = usuario.Telefone,
                Documento = usuario.Documento,
                Tipo = (TipoPessoa)usuario.Tipo,
                Amigos = []
            };

            foreach (var amg in usuario.Amigos)
            {
                usuarioDomain.AdicionarAmigo(
                    ToDomain(amg)!
                );
            }

            return usuarioDomain;
        }

        public static UsuarioDto? ToDto(Usuario usuario)
        {

            if (usuario == null)
            {
                return null;
            }

            UsuarioDto usuarioDto = new()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Senha = usuario.Senha,
                DataComemorativa = usuario.DataComemorativa,
                Sexo = (TipoSexoDto)usuario.Sexo,
                Bio = usuario.Bio,
                FotoPerfil = Encoding.Default.GetString(usuario.FotoPerfil),
                Cidade = usuario.Cidade,
                Uf = (EstadosBrasilDto)usuario.Uf,
                Telefone = usuario.Telefone,
                Documento = usuario.Documento,
                Tipo = (TipoPessoaDto)usuario.Tipo
            };

            List<UsuarioDto> amigos = [];

            foreach (var amg in usuario.Amigos)
            {
                amigos.Add(
                    ToDto(amg)!
                );
            }

            usuarioDto.Amigos = amigos;

            return usuarioDto;

        }

    }
}
