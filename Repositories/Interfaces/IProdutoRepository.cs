using CP2_SOA.Entities;

namespace CP2_SOA.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto?> ObterPorIdAsync(int id);
        Task<List<Produto>> ListarAsync();
        Task AtualizarAsync(Produto produto);
    }
}
