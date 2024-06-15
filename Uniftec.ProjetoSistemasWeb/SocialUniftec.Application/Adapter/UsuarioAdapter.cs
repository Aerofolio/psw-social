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

            Usuario usuarioDomain = new Usuario();
            usuarioDomain.Id = usuario.Id;
            usuarioDomain.Email = usuario.Email;
            usuarioDomain.Nome = usuario.Nome;
            usuarioDomain.Sobrenome = usuario.Sobrenome;
            usuarioDomain.Senha = usuario.Senha;
            usuarioDomain.DataComemorativa = usuario.DataComemorativa;
            usuarioDomain.Sexo = (TipoSexo) usuario.Sexo;
            usuarioDomain.Bio = usuario.Bio;
            usuarioDomain.FotoPefil = usuario.FotoPefil;
            usuarioDomain.Cidade = usuario.Cidade;
            usuarioDomain.Uf = (EstadosBrasil) usuario.Uf;
            usuarioDomain.Telefone = usuario.Telefone;
            usuarioDomain.Documento = usuario.Documento;
            usuarioDomain.Tipo = (TipoPessoa) usuario.Tipo;
            usuarioDomain.Amigos = [];

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

            UsuarioDto usuarioDto = new UsuarioDto();
            usuarioDto.Id = usuario.Id;
            usuarioDto.Email = usuario.Email;
            usuarioDto.Nome = usuario.Nome;
            usuarioDto.Sobrenome = usuario.Sobrenome;
            usuarioDto.Senha = usuario.Senha;
            usuarioDto.DataComemorativa = usuario.DataComemorativa;
            usuarioDto.Sexo = (TipoSexoDto) usuario.Sexo;
            usuarioDto.Bio = usuario.Bio;
            usuarioDto.FotoPefil = usuario.FotoPefil;
            usuarioDto.Cidade = usuario.Cidade;
            usuarioDto.Uf = (EstadosBrasilDto) usuario.Uf;
            usuarioDto.Telefone = usuario.Telefone;
            usuarioDto.Documento = usuario.Documento;
            usuarioDto.Tipo = (TipoPessoaDto) usuario.Tipo;

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
