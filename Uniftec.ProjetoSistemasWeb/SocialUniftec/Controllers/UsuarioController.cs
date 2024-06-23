using Microsoft.AspNetCore.Mvc;
using SocialUniftec.Filtres;
using SocialUniftec.Models;
using SocialUniftec.Website.Backend;
using SocialUniftec.Website.Backend.Adapter;
using SocialUniftec.Website.Backend.HTTPClient;

namespace SocialUniftec.Controllers
{
    public class UsuarioController : Controller
    {
        private static readonly string URLBase = "http://grupo3.neurosky.com.br/api/";
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login) {
            
            
            if (ModelState.IsValid)
            {
                var apiModel = UsuarioAdapter.ToUsuarioLoginModel(login);
                var retorno = new APIHttpClient(URLBase).Post<UsuarioLoginModel, Website.Backend.UsuarioModel>("Usuario/Login", apiModel);
                
                if(retorno is not null)
                {
                    return Redirect("Postagem/Feed");
                }
                else
                {
                    return this.Index();
                }
            } 
            else
            {
                return this.Index();
            }

        }

        public IActionResult Cadastrar() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioCadastroModel usuarioCadastro)
        {
            
            if (usuarioCadastro.Senha != null && usuarioCadastro.SenhaRepetida != null && !usuarioCadastro.Senha.Equals(usuarioCadastro.SenhaRepetida))
            {
                ModelState.AddModelError("senhainvalida", "As senhas não coincidem!");
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioModel = UsuarioAdapter.ToUsuarioModel(usuarioCadastro);
                    
                    var request = new APIHttpClient(URLBase).Post("Usuario", usuarioModel);
                    
                    return Redirect("Postagem/Feed");
                }
                catch (Exception ex)
                {
                    return View("Error");   
                }
            }
            else
            {
                return View("Cadastrar");
            }
        }

        public IActionResult Perfil(Guid id)
        {
            
            // var request = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{id}");
            
            // ViewBag.UsuarioLogado = UsuarioAdapter.ToUsuarioCadastroModel(request);
            
            return View();
        }

        public IActionResult Amigos(int idUsuario)
        {

            // ViewBag.UsuarioLogado = new UsuarioCadastroModel()
            // {
            //     Nome = "Paulo Bodaneze Reva",

            // };

            return View();
        }

        public IActionResult Alterar()
        {
            // ViewBag.UsuarioLogado = new UsuarioCadastroModel()
            // {
            //     Nome = "Paulo Bodaneze Reva",

            // };
            return View(); 
        }

        [ServiceFilter(typeof(ExceptionFilter))]
        public IActionResult Erro()
        {
            throw new Exception("Batata testessssssssssss");
        }

    }

}
