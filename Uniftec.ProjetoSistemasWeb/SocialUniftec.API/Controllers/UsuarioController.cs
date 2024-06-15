using Microsoft.AspNetCore.Mvc;
using SocialUniftec.API.Adapter;
using SocialUniftec.API.Models;
using SocialUniftec.Application.Adapter;
using SocialUniftec.Application.Application;
using SocialUniftec.Application.Dto;
using SocialUniftec.Domain.Entities;
using System.Collections.Generic;

namespace SocialUniftec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController
    {
        [HttpPost]
        public Guid Post(UsuarioModel usuario)
        {
            UsuarioApplication application = new UsuarioApplication();
            return application.Inserir(UsuarioMapping.ToDto(usuario));
        }

        [HttpPut]
        public Guid Put(UsuarioModel usuario)
        {
            UsuarioApplication application = new UsuarioApplication();
            return application.Alterar(UsuarioMapping.ToDto(usuario));
        }

        [HttpDelete("{id:Guid}")]
        public void Delete(Guid id)
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();
            usuarioApplication.Excluir(id);
        }

        [HttpGet("{id:Guid}")]
        public UsuarioModel Get(Guid id)
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();
            UsuarioDto usuario = usuarioApplication.Procurar(id);
            return UsuarioMapping.ToModel(usuario);
        }

        [HttpGet]
        public List<UsuarioModel> ProcurarTodos()
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();
           
            List <UsuarioDto> usuariosDto= usuarioApplication.ProcurarTodos();

            List<UsuarioModel> usuarios = [];

            foreach(var usuarioDto in usuariosDto)
            {
                usuarios.Add(UsuarioMapping.ToModel(usuarioDto));
            }

            return usuarios;
        }

        [HttpGet("ProcurarPorParametros")]
        public ActionResult<List<UsuarioModel>> ProcurarPorParametros([FromQuery] string nome = null, [FromQuery] string sobrenome = null)
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();

            List<UsuarioDto> usuariosDto = usuarioApplication.ProcurarPorParametros(nome, sobrenome);

            List<UsuarioModel> usuarios = new List<UsuarioModel>();

            foreach (var usuarioDto in usuariosDto)
            {
                usuarios.Add(UsuarioMapping.ToModel(usuarioDto));
            }

            return usuarios;
        }

        [HttpPost("Amizade/{id:Guid}/Solicitar/{idUsuarioDestino:Guid}")]
        public Guid EnviarSolicitacaoAmizade(Guid id, Guid idUsuarioDestino)
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();
            return usuarioApplication.EnviarSolicitacaoAmizade(id, idUsuarioDestino);
        }

        [HttpPost("Amizade/{id:Guid}/Aceitar/{idUsuarioAmigo:Guid}")]
        public void AceitarSolicitacaoAmizade(Guid id, Guid idUsuarioAmigo)
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();
            usuarioApplication.AceitarSolicitacaoAmizade(id, idUsuarioAmigo);
        }

        [HttpDelete("Amizade/{id:Guid}/Remover/{idUsuarioAmigo:Guid}")]
        public void RemoverAmizade(Guid id, Guid idUsuarioAmigo)
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();
            usuarioApplication.RemoverAmizade(id, idUsuarioAmigo);
        }

        [HttpPost("Login")]
        public UsuarioModel Login(UsuarioLoginModel usuario)
        {
            UsuarioApplication usuarioApplication = new UsuarioApplication();
            UsuarioDto usuarioDto = usuarioApplication.Login(UsuarioLoginMapping.ToDto(usuario));

            return UsuarioMapping.ToModel(usuarioDto);
        }

    }
}
