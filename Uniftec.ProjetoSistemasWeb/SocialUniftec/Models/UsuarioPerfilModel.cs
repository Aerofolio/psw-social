namespace SocialUniftec.Models
{
    public class UsuarioPerfilModel
    {
        public UsuarioModel UsuarioAutenticado { get; set; } = new UsuarioModel();
        public UsuarioModel UsuarioPerfil { get; set; } = new UsuarioModel();
        public List<FeedModel> ListaFeeds { get; set; } = [];
        public List<UsuarioModel> ListaPrincipaisAmigos { get; set; } = [];
        public bool IsAmigo { get; set; }
    }
}