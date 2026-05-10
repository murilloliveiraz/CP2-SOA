namespace CP2_SOA.DTOs
{
    public class CriarPedidoDTO
    {
        public string Cliente { get; set; } = string.Empty;
        public List<CriarPedidoItemDTO> Itens { get; set; } = new();
    }
}
