using SocialUniftec.Website.Backend;

namespace SocialUniftec.Models
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Senha { get; set; }
        public DateTime DataComemorativa { get; set; }
        public TipoSexoModel Sexo { get; set; }
        public string Bio { get; set; }
        public string FotoPerfil { get; set; }
        public string Cidade { get; set; }
        public EstadosBrasilModel Uf { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }
        public TipoPessoaModel Tipo { get; set; }
        public List<UsuarioModel> Amigos { get; set; }
    }
}
