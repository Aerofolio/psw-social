namespace SocialUniftec.Website.Models
{
    public class PublicacaoIntegracaoModel
    {
        public Guid Id { get; set; }

        public string Usuario { get; set; }

        public string Descricao { get; set; }

        public DateTime DataPublicacao { get; set; }

        public List<PublicacaoMidiaIntegracao> Midias { get; set; }

    }
}
