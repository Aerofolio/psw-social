namespace SocialUniftec.Models
{
    public class FeedModel
    {

        public required UsuarioModel Usuario { get; set; }

        public List<String> ListaURLMidia { get; set; } = [];

        public required string Descricao { get; set; }

        public DateTime DataPublicacao { get; set; }

        public int Curtidas { get; set; }

        public bool IsUsuarioAutenticadoCurtiu { get; set; }

        public List<ComentarioModel> ListaComentarios { get; set; } = [];

    }

}
