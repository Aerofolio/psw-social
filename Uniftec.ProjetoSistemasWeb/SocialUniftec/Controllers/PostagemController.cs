using Microsoft.AspNetCore.Mvc;
using SocialUniftec.Models;

namespace SocialUniftec.Controllers
{
	public class PostagemController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Feed()
		{
			

			List<Story> stories = new List<Story>()
			{
				new Story(1, 1, [], DateTime.Now.AddHours(-1), "Paulo Bodaneze Reva"),
                new Story(2, 1, [], DateTime.Now.AddHours(-1), "Paulo Bodaneze Reva"),
                new Story(3, 2, [], DateTime.Now.AddHours(-2), "João das Neves"),
                new Story(4, 3, [], DateTime.Now.AddHours(-3), "Ayrton Sena"),
                new Story(5, 3, [], DateTime.Now.AddHours(-4), "Ayrton Sena"),
                new Story(6, 4, [], DateTime.Now.AddHours(-3), "Roberto da Silva"),
                new Story(7, 5, [], DateTime.Now.AddHours(-2), "Robson da ZS"),
            };

			List<FeedPost> feedPosts = new List<FeedPost>()
			{
				new FeedPost()
				{
					Id = 1,
					UserId = 1,
					Date =  DateTime.Now.AddHours(-1),
					Likes = 2,
					PostText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc sed hendrerit lacus, sit amet tempor velit. Nulla nec urna semper, tempus velit id, scelerisque tellus. Vestibulum interdum, erat vitae mollis semper, elit metus elementum sapien, at convallis nisl lorem vel lorem. Vestibulum malesuada nibh at ligula mattis, consequat iaculis diam elementum. Nunc euismod turpis eu accumsan scelerisque. Pellentesque eu pretium arcu. Donec maximus ligula eu felis pharetra, vel eleifend orci laoreet. Nulla non justo est. Nullam venenatis, est ac lacinia feugiat, augue tortor bibendum eros, et commodo ligula urna ac lorem. Mauris porttitor pharetra vehicula. Pellentesque ante nisi, gravida sed justo eu, sagittis volutpat ex. Nunc id felis mattis, congue sem quis, luctus lacus. Etiam id risus ut orci dignissim convallis pulvinar non urna. Duis vel nulla velit.",
					UserName = "Paulo Bodaneze Reva",
					UserProfilePicture = [],
					Comentarios = new List<Comentario>()
					{
						new Comentario()
						{
							Id = 1,
							UserId = 2,
							UserName = "João das Neves",
							UserProfilePicture = [],
							Date= DateTime.Now.AddHours(-1),
							Likes= 3,
							Text = "Concordo!"
                        }
					}
                }
			};

            ViewBag.UsuarioLogado = new UsuarioCadastroModel()
            {
                Nome = "Paulo Bodaneze Reva",

            };

			ViewBag.StoriesAgrupados = stories.GroupBy(x => x.UserId).Select(x => new {
				UserId = x.Key,
				UserName = x.FirstOrDefault().UserName,
				LatestStoryDate = - (x.OrderBy(y => y.Date).FirstOrDefault().Date - DateTime.Now).Hours + " Horas Atrás",
				Stories = x.Select(y => new { y.Image, y.Date })
			}
			).ToList();

			ViewBag.Posts = feedPosts;

            return View();
		}

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
