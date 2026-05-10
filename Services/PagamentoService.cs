namespace CP2_SOA.Services
{
    public class PagamentoService
    {
        public bool Processar(decimal valor)
        {
            return valor <= 500;
        }
    }
}
