namespace SocialUniftec.Website.Models
{
    public class ComentarioCadastroModel
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }
        public Guid IdPublicacao { get; set; }
        public string Comentario { get; set; }

    }
}
