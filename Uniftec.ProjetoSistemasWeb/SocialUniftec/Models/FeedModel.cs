namespace SocialUniftec.Models
{
    public class FeedModel
    {
        public Guid Id { get; set; }
        public UsuarioModel Usuario { get; set; } = new UsuarioModel();

        public List<String> ListaMidia { get; set; } = [];

        public required string Descricao { get; set; }

        public DateTime DataPublicacao { get; set; }

        public int Curtidas { get; set; }

        public bool IsUsuarioAutenticadoCurtiu { get; set; }

        public List<ComentarioModel> ListaComentarios { get; set; } = [];

        public FeedModel() { 

        }


    }

}
