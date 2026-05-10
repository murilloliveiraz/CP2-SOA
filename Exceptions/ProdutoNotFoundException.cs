namespace CP2_SOA.Exceptions
{
    public class ProdutoNotFoundException : Exception
    {
        public ProdutoNotFoundException()
            : base("Produto não encontrado.")
        {
        }
    }
}
