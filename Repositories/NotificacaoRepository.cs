namespace CP2_SOA.Repositories
{
    using CP2_SOA.Data;
    using CP2_SOA.Entities;
    using CP2_SOA.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notificacao> SalvarAsync(Notificacao notificacao)
        {
            await _context.Notificacoes.AddAsync(notificacao);
            await _context.SaveChangesAsync();

            return notificacao;
        }

        public async Task<List<Notificacao>> ListarPorPedidoIdAsync(int pedidoId)
        {
            return await _context.Notificacoes
                .Where(n => n.PedidoId == pedidoId)
                .ToListAsync();
        }
    }
}
