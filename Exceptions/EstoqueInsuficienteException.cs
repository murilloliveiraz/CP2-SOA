namespace CP2_SOA.Exceptions
{
    public class EstoqueInsuficienteException : Exception
    {
        public EstoqueInsuficienteException()
            : base("Estoque insuficiente para o produto solicitado.")
        {
        }
    }
}
