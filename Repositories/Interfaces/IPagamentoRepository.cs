using CP2_SOA.Entities;

namespace CP2_SOA.Repositories.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<Pagamento> SalvarAsync(Pagamento pagamento);
        Task<Pagamento?> ObterPorPedidoIdAsync(int pedidoId);
    }
}
