using SocialUniftec.Models;
using SocialUniftec.Website.Models;

namespace SocialUniftec.Website.Backend.Adapter
{
    public class PostagemAdapter
    {

        public static PostagemModel ToPostagemModel(PostagemCadastroModel postagemCadastro)
        {

            List<PublicacaoMidiaIntegracao> Midias = [];

            foreach(var item in postagemCadastro.Midias)
            {
                Midias.Add(new PublicacaoMidiaIntegracao()
                {
                    ContentType = item.ContentType,
                    FileContents = item.FileContents,
                    FileDownloadName = item.FileDownloadName,
                }) ;
            }

            return new()
            {
                Descricao = postagemCadastro.Descricao,
                Midias = Midias,
            };
        }

    }
}