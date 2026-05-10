using CP2_SOA.DTOs;
using CP2_SOA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CP2_SOA.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;
        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPedidoDTO dto)
        {
            var pedido = await _pedidoService.CriarPedido(dto);

            return Created($"/api/pedidos/{pedido.Id}", pedido);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var pedido = await _pedidoService.ObterPorId(id);

            return Ok(pedido);
        }
    }
}
