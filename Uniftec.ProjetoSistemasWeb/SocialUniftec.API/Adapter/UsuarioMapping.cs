using SocialUniftec.API.Models;
using SocialUniftec.Application.Dto;
using SocialUniftec.Domain.Entities;

namespace SocialUniftec.Application.Adapter
{
    public class UsuarioMapping
    {

        public static UsuarioModel? ToModel(UsuarioDto? usuario)
        {

            if (usuario == null)
            {
                return null;
            }

            UsuarioModel usuarioModel = new()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Senha = usuario.Senha,
                DataComemorativa = usuario.DataComemorativa,
                Sexo = (TipoSexoModel)usuario.Sexo,
                Bio = usuario.Bio,
                FotoPerfil = usuario.FotoPerfil,
                Cidade = usuario.Cidade,
                Uf = (EstadosBrasilModel)usuario.Uf,
                Telefone = usuario.Telefone,
                Documento = usuario.Documento,
                Tipo = (TipoPessoaModel)usuario.Tipo
            };

            List<UsuarioModel> amigos = new List<UsuarioModel>();

            foreach (var amg in usuario.Amigos)
            {
                amigos.Add(
                    ToModel(amg)!
                );
            }

            usuarioModel.Amigos = amigos;

            return usuarioModel;
        }

        public static UsuarioDto? ToDto(UsuarioModel usuario)
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
                FotoPerfil = usuario.FotoPerfil,
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
