namespace CP2_SOA.Repositories
{
    using CP2_SOA.Data;
    using CP2_SOA.Entities;
    using CP2_SOA.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Produto>> ListarAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}
