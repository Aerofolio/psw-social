namespace SocialUniftec.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string? UrlFoto { get; set; }

        public string? Cidade { get; set; }

        public string? Uf { get; set;}

        public string? Bio { get; set; }

        public string? telefone { get; set; }
    }
}
