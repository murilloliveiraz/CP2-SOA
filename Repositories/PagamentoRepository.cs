namespace CP2_SOA.Repositories
{
    using CP2_SOA.Data;
    using CP2_SOA.Entities;
    using CP2_SOA.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pagamento> SalvarAsync(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();

            return pagamento;
        }

        public async Task<Pagamento?> ObterPorPedidoIdAsync(int pedidoId)
        {
            return await _context.Pagamentos
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
        }
    }
}
