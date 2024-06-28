using Microsoft.AspNetCore.Mvc;

namespace SocialUniftec.Website.Models
{
    public class PostagemModel
    {
        public Guid Id { get; set; }
        public Guid Usuario { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPublicacao { get; set; }

        public List<IFormFile> Midias { get; set; }
    }
}
