namespace SocialUniftec.Website.Models
{
    public class PostagemCadastroModel
    {
        public Guid? Id { get; set; }
        public string? Usuario { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public List<PostagemCadastroMidiaModel>? Midias { get; set; }
    }
}
