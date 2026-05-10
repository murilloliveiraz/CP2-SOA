namespace CP2_SOA.Exceptions
{
    public class PedidoSemItensException : Exception
    {
        public PedidoSemItensException()
            : base("O pedido deve possuir pelo menos um item.")
        {
        }
    }
}
