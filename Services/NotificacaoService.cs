namespace CP2_SOA.Services
{
    public class NotificacaoService
    {
        public void Notificar(string mensagem)
        {
            Console.WriteLine($"[NOTIFICACAO] {mensagem}");
        }
    }
}
