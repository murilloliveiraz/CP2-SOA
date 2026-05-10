using CP2_SOA.Entities;

namespace CP2_SOA.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> SalvarAsync(Pedido pedido);
        Task<Pedido?> ObterPorIdAsync(int id);
        Task<List<Pedido>> ListarAsync();
        Task AtualizarAsync(Pedido pedido);
    }
}
