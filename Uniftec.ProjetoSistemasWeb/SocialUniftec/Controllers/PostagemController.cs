using System.Runtime.Intrinsics.Arm;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialUniftec.Models;
using SocialUniftec.Website.Backend.HTTPClient;
using SocialUniftec.Website.Backend;
using SocialUniftec.Website.Models;
using SocialUniftec.Website.Backend.Adapter;
using System.Collections.Generic;

namespace SocialUniftec.Controllers
{
    public class PostagemController : Controller
    {
       
        private static readonly string URLBasePublicacao = "http://grupo5.neurosky.com.br/api/";
        private static readonly string URLBaseUsuario = "http://grupo3.neurosky.com.br/api/";
        private static readonly string URLBaseLikeEComentario = "http://grupo4.neurosky.com.br/api/";
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Feed()
        {

            var usuarioLogado = ObterUsuarioLogado();

            ViewBag.Posts = buscarListaPost(usuarioLogado);
            ViewBag.StoriesAgrupados = new List<Story>();
            ViewBag.UsuarioLogado = usuarioLogado;

            return View();
        }

        [HttpPost]
        public Guid Cadastrar([FromBody] PostagemCadastroModel postagemCadastro)
        {
            var postagemModel = PostagemAdapter.ToPostagemModel(postagemCadastro);
            postagemModel.DataPublicacao = DateTime.Now;
            var usuarioLogado = ObterUsuarioLogado();
            postagemModel.Usuario = usuarioLogado.Id;

            var id = new APIHttpClient(URLBasePublicacao).Post("Publicacao?Usuario=" + postagemModel.Usuario + "&Descricao=" + postagemModel.Descricao + "&DataPublicacao=" + postagemModel.DataPublicacao.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), postagemModel);

            return id;
 
        }

        private List<FeedModel> buscarListaPost(Website.Backend.UsuarioModel usuarioLogado) {

            var publicacoes = new APIHttpClient(URLBasePublicacao).Get<List<PublicacaoIntegracaoModel>>("Publicacao?idUsuario=" + usuarioLogado.Id);

            List<FeedModel> feeds = new List<FeedModel>();

            foreach (var item in publicacoes)
            {

                //buscar curtidas
                //buscar curtidas dos comentarios

                List<String> midias = new List<String>();

                foreach (var midia in item.Midias)
                {
                    midias.Add(midia.FileContents);
                }

                Models.UsuarioModel usuarioDono = buscarUsuarioModelPorIdUsuario(item.Usuario);

                List<ComentarioModel> comentarios = [];//buscarListaComentariosPorPostId(item.Id);

                FeedModel feedModel = new FeedModel()
                {
                    Descricao = item.Descricao,
                    Curtidas = 0,
                    DataPublicacao = item.DataPublicacao,
                    IsUsuarioAutenticadoCurtiu = false,
                    ListaComentarios = comentarios,
                    ListaMidia = midias,
                    Usuario = usuarioDono
                };

                feeds.Add(feedModel);

            }

            return feeds;
        }

        private List<ComentarioModel> buscarListaComentariosPorPostId(Guid id)
        {

            List<ComentarioIntegracaoModel> comentariosPost = new APIHttpClient(URLBaseLikeEComentario).Get<List<ComentarioIntegracaoModel>>($"comentarios/post/{id}");

            List<ComentarioModel> comentarios = [];

            foreach (var item in comentariosPost)
            {

                var usuarioComentario = buscarUsuarioModelPorIdUsuario(item.IdUsuario);

                List<ComentarioModel> respostas = buscarRespostasRecursivamentePorComentarioId(item.Id);

                comentarios.Add(new ComentarioModel()
                {
                    Comentario = item.Conteudo,
                    Curtidas = item.QuantidadeLikes,
                    DataComentario = item.DataCriacao,
                    Usuario = usuarioComentario,
                    IsUsuarioAutenticadoCurtiu = false,
                    ListaComentarios = respostas
                }); ;
            }

            return comentarios;
        }

        private List<ComentarioModel> buscarRespostasRecursivamentePorComentarioId(Guid id)
        {
            List<ComentarioIntegracaoModel> comentariosPost = new APIHttpClient(URLBaseLikeEComentario).Get<List<ComentarioIntegracaoModel>>($"comentarios/resposta/{id}");

            List<ComentarioModel> comentarios = [];

            foreach (var item in comentariosPost)
            {

                var usuarioComentario = buscarUsuarioModelPorIdUsuario(item.IdUsuario);

                List<ComentarioModel> respostas = buscarRespostasRecursivamentePorComentarioId(item.Id);

                comentarios.Add(new ComentarioModel()
                {
                    Comentario = item.Conteudo,
                    Curtidas = item.QuantidadeLikes,
                    DataComentario = item.DataCriacao,
                    Usuario = usuarioComentario,
                    IsUsuarioAutenticadoCurtiu = false,
                    ListaComentarios = respostas
                }); ;
            }

            return comentarios;
        }

        private Models.UsuarioModel buscarUsuarioModelPorIdUsuario(Guid id)
        {
            var user = new APIHttpClient(URLBaseUsuario).Get<Models.UsuarioModel>($"Usuario/{id}");
            return user;
        }

        private Website.Backend.UsuarioModel? ObterUsuarioLogado() =>
            JsonConvert.DeserializeObject<Website.Backend.UsuarioModel>(Encoding.UTF8.GetString(HttpContext.Session.Get("UsuarioLogado")));

        public class Story
        {
            public Story(int id, int userId, byte[] image, DateTime date, string userName)
            {
                Id = id;
                UserId = userId;
                Image = image;
                Date = date;
                UserName = userName;
            }

            public int Id { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public byte[] Image { get; set; }

            public DateTime Date { get; set; }
        }

    }
}
