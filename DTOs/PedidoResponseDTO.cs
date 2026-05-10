namespace CP2_SOA.DTOs
{
    public class PedidoResponseDTO
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<PedidoItemResponseDTO> Itens { get; set; } = new();
    }
}
