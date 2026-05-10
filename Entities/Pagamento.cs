namespace CP2_SOA.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public decimal Valor { get; set; }
        public StatusPagamento Status { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public DateTime? DataPagamento { get; set; }
    }
}
