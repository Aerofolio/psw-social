using Microsoft.AspNetCore.Mvc;

namespace SocialUniftec.Website.Models
{
    public class PostagemCadastroModel
    {
        public string Descricao { get; set; }

        public Guid IdUsuario { get; set; }

        [BindProperty]
        public IFormFile MidiaPostagem { get; set; }
    }
}
