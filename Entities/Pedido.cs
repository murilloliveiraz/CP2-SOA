namespace CP2_SOA.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
        public List<PedidoItem> Itens { get; set; } = new();
    }
}
