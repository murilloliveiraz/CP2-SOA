using CP2_SOA.Entities;

namespace CP2_SOA.Repositories.Interfaces
{
    public interface INotificacaoRepository
    {
        Task<Notificacao> SalvarAsync(Notificacao notificacao);
        Task<List<Notificacao>> ListarPorPedidoIdAsync(int pedidoId);
    }
}
