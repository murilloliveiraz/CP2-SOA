using CP2_SOA.DTOs;
using CP2_SOA.Entities;
using CP2_SOA.Exceptions;
using CP2_SOA.Repositories;
using CP2_SOA.Repositories.Interfaces;

namespace CP2_SOA.Services
{
    public class PedidoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly PagamentoService _pagamentoService;
        private readonly EstoqueService _estoqueService;
        private readonly NotificacaoService _notificacaoService;

        public PedidoService(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, PagamentoService pagamentoService, EstoqueService estoqueService, NotificacaoService notificacaoService)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _pagamentoService = pagamentoService;
            _estoqueService = estoqueService;
            _notificacaoService = notificacaoService;
        }

        public async Task<Pedido> CriarPedido(CriarPedidoDTO dto)
        {
            if (dto.Itens == null || !dto.Itens.Any())
                throw new Exception("Pedido sem itens");

            var pedido = new Pedido
            {
                Cliente = dto.Cliente,
                Status = StatusPedido.CRIADO
            };

            decimal total = 0;

            foreach (var item in dto.Itens)
            {
                var produto = await _produtoRepository.ObterPorIdAsync(item.ProdutoId);

                if (produto == null)
                    throw new ProdutoNotFoundException();

                _estoqueService.ValidarEstoque(produto, item.Quantidade);

                pedido.Itens.Add(new PedidoItem
                {
                    ProdutoId = produto.Id,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produto.Preco,
                    Subtotal = produto.Preco * item.Quantidade
                });

                total += produto.Preco * item.Quantidade;
            }

            pedido.ValorTotal = total;
            pedido.Status = StatusPedido.AGUARDANDO_PAGAMENTO;

            var pagamentoAprovado = _pagamentoService.Processar(total);

            if (!pagamentoAprovado)
            {
                pedido.Status = StatusPedido.CANCELADO;

                _notificacaoService.Notificar("Pagamento recusado");

                throw new PagamentoRecusadoException();
            }

            pedido.Status = StatusPedido.PAGO;

            foreach (var item in dto.Itens)
            {
                var produto = await _produtoRepository.ObterPorIdAsync(item.ProdutoId);

                _estoqueService.ReduzirEstoque(produto, item.Quantidade);
            }

            pedido.Status = StatusPedido.FINALIZADO;

            await _pedidoRepository.SalvarAsync(pedido);

            _notificacaoService.Notificar("Pedido finalizado");

            return pedido;
        }

        public async Task<Pedido> ObterPorId(int id)
        {
            if(id <= 0)
                throw new Exception("Id inválido");

            var pedido = await _pedidoRepository.ObterPorIdAsync(id);

            if (pedido == null){
                throw new Exception("Pedido não existe");
            }

            return pedido;
        }
    }
}
