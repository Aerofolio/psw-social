using System.Runtime.Intrinsics.Arm;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialUniftec.Models;
using SocialUniftec.Website.Backend.HTTPClient;
using SocialUniftec.Website.Backend;
using SocialUniftec.Website.Models;

namespace SocialUniftec.Controllers
{
    public class PostagemController : Controller
    {
       
        private static readonly string URLBasePublicacao = "http://grupo5.neurosky.com.br/api/";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Feed()
        {

            var usuarioLogado = ObterUsuarioLogado();

            ViewBag.Posts = buscarListaPost(usuarioLogado);
            ViewBag.StoriesAgrupados = new List<Story>();

            return View();
        }

        private List<FeedModel> buscarListaPost(Website.Backend.UsuarioModel usuarioLogado) {

            var publicacoes = new APIHttpClient(URLBasePublicacao).Get<List<PublicacaoIntegracaoModel>>("Publicacao?idUsuario=" + usuarioLogado.Id);

            List<FeedModel> feeds = new List<FeedModel>();

            foreach (var item in publicacoes)
            {

                //buscar curtidas
                //buscar comentarios
                //buscar curtidas dos comentarios
                //buscar comentarios dos comentarios
                //loop

                //converter para objeto feedModel

                List<String> midias = new List<String>();

                foreach (var midia in item.Midias)
                {
                    midias.Add(midia.FileContents);
                }

                FeedModel feedModel = new FeedModel()
                {
                    Descricao = item.Descricao,
                    Curtidas = 0,
                    DataPublicacao = item.DataPublicacao,
                    IsUsuarioAutenticadoCurtiu = false,
                    ListaComentarios = [],
                    ListaMidia = midias,
                    Usuario = new Models.UsuarioModel() { }
                };

                feeds.Add(feedModel);

            }

            return feeds;
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

        public class FeedPost
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public byte[] UserProfilePicture { get; set; }
            public string PostText { get; set; }
            public int Likes { get; set; }
            public DateTime Date { get; set; }
            public List<Comentario> Comentarios { get; set; }

        }

        public class Comentario 
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public byte[] UserProfilePicture { get; set; }
            public string Text { get; set; }
            public int Likes { get; set; }
            public DateTime Date { get; set; }
        }


    }
}
