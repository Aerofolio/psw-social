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

            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Id = usuario.Id;
            usuarioModel.Email = usuario.Email;
            usuarioModel.Nome = usuario.Nome;
            usuarioModel.Sobrenome = usuario.Sobrenome;
            usuarioModel.Senha = usuario.Senha;
            usuarioModel.DataComemorativa = usuario.DataComemorativa;
            usuarioModel.Sexo = (TipoSexoModel) usuario.Sexo;
            usuarioModel.Bio = usuario.Bio;
            usuarioModel.FotoPefil = usuario.FotoPefil;
            usuarioModel.Cidade = usuario.Cidade;
            usuarioModel.Uf = (EstadosBrasilModel) usuario.Uf;
            usuarioModel.Telefone = usuario.Telefone;
            usuarioModel.Documento = usuario.Documento;
            usuarioModel.Tipo = (TipoPessoaModel) usuario.Tipo;

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
