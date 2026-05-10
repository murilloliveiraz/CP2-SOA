namespace CP2_SOA.Exceptions
{
    public class PagamentoRecusadoException : Exception
    {
        public PagamentoRecusadoException()
            : base("Pagamento recusado.")
        {
        }
    }
}
