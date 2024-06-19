using Microsoft.AspNetCore.Mvc;
using SocialUniftec.Filtres;
using SocialUniftec.Models;
using SocialUniftec.Website.Backend.HTTPClient;

namespace SocialUniftec.Controllers
{
    public class UsuarioController : Controller
    {
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
                return Redirect("Postagem/Feed");
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
                    var a = new Website.Backend.UsuarioModel()
                    {
                        Nome = usuarioCadastro.Nome,
                        Sobrenome = usuarioCadastro.Sobrenome,
                        Email = usuarioCadastro.Email,
                        Senha = usuarioCadastro.Senha,
                        DataComemorativa = usuarioCadastro.DataComemorativa,
                        Sexo = Website.Backend.TipoSexoModel.Masculino,
                        Bio = "teste",
                        FotoPerfil = string.Empty,
                        Cidade = "teste",
                        Uf = Website.Backend.EstadosBrasilModel.AC,
                        Telefone = "teste",
                        Documento = "teste",
                        Tipo = Website.Backend.TipoPessoaModel.Juridica,
                        Amigos = []
                    };
                    
                    var request = new APIHttpClient("http://localhost:5048/api/").Post("Usuario", a);
                    
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

        public IActionResult Perfil(int idUsuario)
        {

            ViewBag.UsuarioLogado = new UsuarioCadastroModel()
            {
                Nome = "Paulo Bodaneze Reva",

            };

            //UsuarioPerfilModel


            return View();
        }

        public IActionResult Amigos(int idUsuario)
        {

            ViewBag.UsuarioLogado = new UsuarioCadastroModel()
            {
                Nome = "Paulo Bodaneze Reva",

            };

            return View();
        }

        public IActionResult Alterar()
        {
            ViewBag.UsuarioLogado = new UsuarioCadastroModel()
            {
                Nome = "Paulo Bodaneze Reva",

            };
            return View(); 
        }

        [ServiceFilter(typeof(ExceptionFilter))]
        public IActionResult Erro()
        {
            throw new Exception("Batata testessssssssssss");
        }

    }

}
