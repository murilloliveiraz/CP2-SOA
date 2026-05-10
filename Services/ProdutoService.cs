using CP2_SOA.Entities;
using CP2_SOA.Exceptions;
using CP2_SOA.Repositories.Interfaces;

namespace CP2_SOA.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<Produto>> ListarAsync()
        {
            return await _produtoRepository.ListarAsync();
        }

        public async Task<Produto> ObterPorIdAsync(int id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto == null)
                throw new ProdutoNotFoundException();

            return produto;
        }
    }
}
