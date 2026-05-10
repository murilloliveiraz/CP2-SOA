using CP2_SOA.Entities;
using CP2_SOA.Exceptions;

namespace CP2_SOA.Services
{
    public class EstoqueService
    {
        public void ValidarEstoque(Produto produto, int quantidade)
        {
            if (produto.QuantidadeEstoque < quantidade)
                throw new EstoqueInsuficienteException();
        }

        public void ReduzirEstoque(Produto produto, int quantidade)
        {
            produto.QuantidadeEstoque -= quantidade;
        }
    }
}
