namespace CP2_SOA.Entities
{
    public class Notificacao
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; } = DateTime.UtcNow;
    }
}
