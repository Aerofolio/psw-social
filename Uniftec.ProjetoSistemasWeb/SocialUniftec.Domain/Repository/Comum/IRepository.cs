namespace SocialUniftec.Repository.Repository.Comum
{
	public interface IRepository<TEntidade> where TEntidade : class
    {
    	void Inserir(TEntidade usuario);
        void Alterar(TEntidade usuario);
        void Excluir(Guid id);
        TEntidade Procurar(Guid id);
        List<TEntidade> ProcurarTodos();
    }
}