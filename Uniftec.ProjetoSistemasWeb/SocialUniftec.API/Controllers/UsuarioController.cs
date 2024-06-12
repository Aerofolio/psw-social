using Microsoft.AspNetCore.Mvc;
using SocialUniftec.API.Models;
using SocialUniftec.Application.Adapter;
using SocialUniftec.Application.Application;
using SocialUniftec.Application.Dto;

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

        //TODO enviar solicitacao amizade
        //TODO aceitar solicitacao amizade
        //TODO remover amizade

    }
}
