namespace CP2_SOA.Exceptions
{
    public class StatusPedidoInvalidoException : Exception
    {
        public StatusPedidoInvalidoException()
            : base("Status do pedido inválido para essa operação.")
        {
        }
    }
}
