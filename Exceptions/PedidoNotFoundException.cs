namespace CP2_SOA.Exceptions
{
    public class PedidoNotFoundException : Exception
    {
        public PedidoNotFoundException()
            : base("Pedido não encontrado.")
        {
        }
    }
}
