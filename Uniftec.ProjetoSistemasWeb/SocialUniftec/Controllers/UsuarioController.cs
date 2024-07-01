using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialUniftec.Filtres;
using SocialUniftec.Models;
using SocialUniftec.Website.Backend;
using SocialUniftec.Website.Backend.Adapter;
using SocialUniftec.Website.Backend.HTTPClient;
using SocialUniftec.Website.Models;
using static SocialUniftec.Controllers.PostagemController;

namespace SocialUniftec.Controllers
{
    public class UsuarioController : Controller
    {
        private static readonly string URLBase = "http://grupo3.neurosky.com.br/api/";
        private static readonly string URLBasePublicacao = "http://grupo5.neurosky.com.br/api/";
        private static readonly string URLBaseLikeEComentario = "http://grupo4.neurosky.com.br/api/";
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public void CurtirPostagem(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBaseLikeEComentario).Post("likes/post/" + id + "/" + usuarioLogado.Id);
        }

        [HttpDelete]
        public void ExcluirPostagem(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBasePublicacao).Delete("Publicacao?id=" + id);
        }

        [HttpDelete]
        public void DescurtirPostagem(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBaseLikeEComentario).Delete("likes/post/" + id + "/" + usuarioLogado.Id);
        }

        [HttpPost]
        public void CurtirComentario(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBaseLikeEComentario).Post("likes/comentario/" + id + "/" + usuarioLogado.Id);
        }

        [HttpDelete]
        public void DescurtirComentario(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBaseLikeEComentario).Delete("likes/comentario/" + id + "/" + usuarioLogado.Id);
        }

        [HttpPost]
        public IActionResult Responder(ComentarioCadastroModel comentarioCadastro)
        {
            if (ModelState.IsValid)
            {
                var usuarioLogado = ObterUsuarioLogado();

                var comentarioModel = PostagemAdapter.ToComentarioIntegracaoModel(comentarioCadastro);
                comentarioModel.DataCriacao = DateTime.Now;
                comentarioModel.QuantidadeLikes = 0;
                comentarioModel.IdUsuario = usuarioLogado.Id;

                var id = new APIHttpClient(URLBaseLikeEComentario).Post("comentarios/resposta/" + comentarioCadastro.Id, comentarioModel);

                ViewBag.UsuarioLogado = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{usuarioLogado.Id}");
                ViewBag.UsuarioSendoVisto = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{comentarioCadastro.IdUsuario}");
                ViewBag.Posts = buscarListaPost(comentarioCadastro.IdUsuario, usuarioLogado);

                return View("perfil");

            }
            else
            {
                return View("erro");
            }
        }

        [HttpPost]
        public IActionResult Comentar(ComentarioCadastroModel comentarioCadastro)
        {
            if (ModelState.IsValid)
            {
                var usuarioLogado = ObterUsuarioLogado();

                var comentarioModel = PostagemAdapter.ToComentarioIntegracaoModel(comentarioCadastro);
                comentarioModel.DataCriacao = DateTime.Now;
                comentarioModel.QuantidadeLikes = 0;
                comentarioModel.IdUsuario = usuarioLogado.Id;

                var id = new APIHttpClient(URLBaseLikeEComentario).Post("comentarios/post/" + comentarioCadastro.IdPublicacao, comentarioModel);

                ViewBag.UsuarioLogado = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{usuarioLogado.Id}");
                ViewBag.UsuarioSendoVisto = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{comentarioCadastro.IdUsuario}");
                ViewBag.Posts = buscarListaPost(comentarioCadastro.IdUsuario, usuarioLogado);

                return View("perfil");

            }
            else
            {
                return View("erro");
            }

        }

        [HttpPost]
        public IActionResult CadastrarPostagem(PostagemCadastroModel postagemCadastro)
        {
            var postagemModel = PostagemAdapter.ToPostagemModel(postagemCadastro);
            postagemModel.DataPublicacao = DateTime.Now;
            var usuarioLogado = ObterUsuarioLogado();
            postagemModel.Usuario = usuarioLogado.Id;

            var id = new APIHttpClient(URLBasePublicacao).Post("Publicacao?Usuario=" + postagemModel.Usuario + "&Descricao=" + postagemModel.Descricao + "&DataPublicacao=" + postagemModel.DataPublicacao.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), postagemModel);

            ViewBag.UsuarioLogado = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{usuarioLogado.Id}");
            ViewBag.UsuarioSendoVisto = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{postagemCadastro.IdUsuario}");
            ViewBag.Posts = buscarListaPost(postagemCadastro.IdUsuario, usuarioLogado);

            return View("perfil");

        }

        [HttpPost]
        public IActionResult Login(LoginModel login) {
            if (ModelState.IsValid)
            {
                var apiModel = UsuarioAdapter.ToUsuarioLoginModel(login);
                var retorno = new APIHttpClient(URLBase).Post<UsuarioLoginModel, Website.Backend.UsuarioModel>("Usuario/Login", apiModel);
                
                if(retorno is not null)
                {
                    HttpContext.Session.Set("UsuarioLogado", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(retorno)));
                    ViewBag.UsuarioLogado = UsuarioAdapter.ToUsuarioCadastroModel(retorno);
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
                    
                    return View("Login");
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
            var usuarioLogado = ObterUsuarioLogado();

            ViewBag.UsuarioLogado = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{usuarioLogado.Id}");
            ViewBag.UsuarioSendoVisto = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{id}");
            ViewBag.Posts = buscarListaPost(id, usuarioLogado);

            return View();
        }
        
        [HttpGet]
        public IActionResult Buscar(string palavraBusca)
        {
            var palavrasBuscar = string.IsNullOrEmpty(palavraBusca) ? [] : palavraBusca.Split(" ", 2);
            var queryBuscar = "Usuario/ProcurarPorParametros?";
            
            if (palavrasBuscar.Length > 0 && !string.IsNullOrEmpty(palavrasBuscar[0]))
                queryBuscar += $"nome={palavrasBuscar[0]}";
                
            if (palavrasBuscar.Length > 1 && !string.IsNullOrEmpty(palavrasBuscar[1]))
                queryBuscar += $"sobrenome={palavrasBuscar[1]}";
            
            ViewBag.ListaUsuariosBuscar = new APIHttpClient(URLBase).Get<List<Website.Backend.UsuarioModel>>(queryBuscar);
            
            return View();
        }

        private List<FeedModel> buscarListaPost(Guid idUsuarioPerfil, Website.Backend.UsuarioModel usuarioLogado)
        {
            List<PublicacaoIntegracaoModel> listaTodasPublicacoes = new APIHttpClient(URLBasePublicacao).Get<List<PublicacaoIntegracaoModel>>("Publicacao?idUsuario=" + idUsuarioPerfil);

            List<FeedModel> feeds = new List<FeedModel>();

            foreach (var item in listaTodasPublicacoes)
            {

                List<Guid> listaIdsUsuariosCurtiramOPost = buscarListaUsuariosCurtiramOPost(item.Id);

                List<String> midias = new List<String>();

                foreach (var midia in item.Midias)
                {
                    midias.Add(midia.FileContents);
                }

                Models.UsuarioModel usuarioDono = buscarUsuarioModelPorIdUsuario(item.Usuario);

                List<ComentarioModel> comentarios = buscarListaComentariosPorPostId(item.Id, usuarioLogado);

                FeedModel feedModel = new FeedModel()
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Curtidas = listaIdsUsuariosCurtiramOPost.Count,
                    DataPublicacao = item.DataPublicacao,
                    IsUsuarioAutenticadoCurtiu = validarSeUsuarioCurtiu(listaIdsUsuariosCurtiramOPost, usuarioLogado.Id),
                    ListaComentarios = comentarios,
                    ListaMidia = midias,
                    Usuario = usuarioDono
                };

                feeds.Add(feedModel);

            }

            return feeds;
        }

        private List<Guid> buscarListaUsuariosCurtiramOPost(Guid id)
        {
            List<Guid> listaIdsUsuariosCurtiramOPost = new APIHttpClient(URLBaseLikeEComentario).Get<List<Guid>>($"likes/post/{id}");

            return listaIdsUsuariosCurtiramOPost;
        }

        private bool validarSeUsuarioCurtiu(List<Guid> listaIdsUsuariosCurtiramOPost, Guid id)
        {
            bool isUsuarioCurtiu = false;

            foreach (var idUsuarioCurtiu in listaIdsUsuariosCurtiramOPost)
            {
                if (idUsuarioCurtiu.Equals(id))
                {
                    isUsuarioCurtiu = true;
                    break;
                }
            }
            return isUsuarioCurtiu;
        }

        private List<ComentarioModel> buscarListaComentariosPorPostId(Guid id, Website.Backend.UsuarioModel usuarioLogado)
        {

            List<ComentarioIntegracaoModel> comentariosPost = new APIHttpClient(URLBaseLikeEComentario).Get<List<ComentarioIntegracaoModel>>($"comentarios/post/{id}");

            List<ComentarioModel> comentarios = [];

            foreach (var item in comentariosPost)
            {

                var usuarioComentario = buscarUsuarioModelPorIdUsuario(item.IdUsuario);

                List<Guid> listaUsuariosCurtiramComentario = buscarListaUsuariosCurtiramOComentario(item.Id);

                List<ComentarioModel> respostas = buscarRespostasRecursivamentePorComentarioId(item.Id, usuarioLogado);

                comentarios.Add(new ComentarioModel()
                {
                    Id = item.Id,
                    Comentario = item.Conteudo,
                    Curtidas = listaUsuariosCurtiramComentario.Count,
                    DataComentario = item.DataCriacao,
                    Usuario = usuarioComentario,
                    IsUsuarioAutenticadoCurtiu = validarSeUsuarioCurtiu(listaUsuariosCurtiramComentario, usuarioLogado.Id),
                    ListaComentarios = respostas
                }); ;
            }

            return comentarios;
        }

        private List<ComentarioModel> buscarRespostasRecursivamentePorComentarioId(Guid id, Website.Backend.UsuarioModel usuarioLogado)
        {
            List<ComentarioIntegracaoModel> comentariosPost = new APIHttpClient(URLBaseLikeEComentario).Get<List<ComentarioIntegracaoModel>>($"comentarios/resposta/{id}");

            List<ComentarioModel> comentarios = [];

            foreach (var item in comentariosPost)
            {

                var usuarioComentario = buscarUsuarioModelPorIdUsuario(item.IdUsuario);

                List<ComentarioModel> respostas = [];

                if (item.Id != id)
                {
                    respostas = buscarRespostasRecursivamentePorComentarioId(item.Id, usuarioLogado);
                }

                List<Guid> listaUsuariosCurtiramComentario = buscarListaUsuariosCurtiramOComentario(item.Id);

                comentarios.Add(new ComentarioModel()
                {
                    Id = item.Id,
                    Comentario = item.Conteudo,
                    Curtidas = listaUsuariosCurtiramComentario.Count,
                    DataComentario = item.DataCriacao,
                    Usuario = usuarioComentario,
                    IsUsuarioAutenticadoCurtiu = validarSeUsuarioCurtiu(listaUsuariosCurtiramComentario, usuarioLogado.Id),
                    ListaComentarios = respostas
                }); ;
            }

            return comentarios;
        }

        private List<Guid> buscarListaUsuariosCurtiramOComentario(Guid id)
        {

            List<Guid> listaIdsUsuariosCurtiramOComentario = new APIHttpClient(URLBaseLikeEComentario).Get<List<Guid>>($"likes/comentario/{id}");

            return listaIdsUsuariosCurtiramOComentario;
        }

        private Models.UsuarioModel buscarUsuarioModelPorIdUsuario(Guid id)
        {
            var user = new APIHttpClient(URLBase).Get<Models.UsuarioModel>($"Usuario/{id}");
            return user;
        }

        public IActionResult Amigos(Guid id)
        {
            ViewBag.UsuarioLogado = ObterUsuarioLogado();
            var usuarioSendoVisto = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{id}");
            ViewBag.UsuarioSendoVisto = usuarioSendoVisto;
            
            return View();
        }
        
        public IActionResult RemoverAmigoViewAmigos(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBase).Delete($"Usuario/Amizade/{usuarioLogado.Id}/Remover/", id);
            
            usuarioLogado = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{usuarioLogado.Id}");
            
            ViewBag.UsuarioLogado = usuarioLogado;
            ViewBag.UsuarioSendoVisto = usuarioLogado;
            
            return View("Amigos");
        }
        
        public IActionResult RemoverAmigoViewPerfil(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBase).Delete($"Usuario/Amizade/{usuarioLogado.Id}/Remover/", id);
            
            usuarioLogado = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{usuarioLogado.Id}");
            
            ViewBag.UsuarioLogado = usuarioLogado;
            ViewBag.UsuarioSendoVisto = usuarioLogado;
            
            return RedirectToAction($"Perfil", new {id = id});
        }
        
        public IActionResult EnviarSolicitacaoDeAmizade(Guid id)
        {
            var usuarioLogado = ObterUsuarioLogado();
            new APIHttpClient(URLBase).Post($"Usuario/Amizade/{usuarioLogado.Id}/Solicitar/{id}");
                        
            return RedirectToAction($"Perfil", new {id = id});
        }

        public IActionResult Alterar()
        {
            ViewBag.UsuarioLogado = ObterUsuarioLogado();
            
            return View(); 
        }
        
        [HttpPost]
        public IActionResult AlterarPerfil(UsuarioAlterarModel model)
        {
            var usuarioLogado = ObterUsuarioLogado();
            model.Id = usuarioLogado.Id;
            model.Senha = usuarioLogado?.Senha ?? string.Empty;
            
            var usuarioAlteradoModel = UsuarioAdapter.ToUsuarioModel(model);
            usuarioAlteradoModel.Amigos = usuarioLogado.Amigos;
            usuarioAlteradoModel.FotoPerfil ??= usuarioLogado.FotoPerfil;
            
            new APIHttpClient(URLBase).Put("Usuario", usuarioAlteradoModel);

            var usuarioAlterado = new APIHttpClient(URLBase).Get<Website.Backend.UsuarioModel>($"Usuario/{model.Id}");
            HttpContext.Session.Set("UsuarioLogado", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(usuarioAlterado)));    
            
            return Redirect($"Perfil/{usuarioAlterado.Id}");
        }

        [ServiceFilter(typeof(ExceptionFilter))]
        public IActionResult Erro()
        {
            throw new Exception("Batata testessssssssssss");
        }
        
        private Website.Backend.UsuarioModel? ObterUsuarioLogado() => 
            JsonConvert.DeserializeObject<Website.Backend.UsuarioModel>(Encoding.UTF8.GetString(HttpContext.Session.Get("UsuarioLogado")));

    }

}
