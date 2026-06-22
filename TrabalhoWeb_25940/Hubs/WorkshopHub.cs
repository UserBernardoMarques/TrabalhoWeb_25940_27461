using Microsoft.AspNetCore.SignalR;

namespace TrabalhoWeb_25940.Hubs
{
    // Este é o motor que permite enviar avisos para todos os utilizadores ao mesmo tempo
    public class WorkshopHub : Hub
    {
        public async Task EnviarNotificacao(string mensagem)
        {
            await Clients.All.SendAsync("ReceberNotificacao", mensagem);
        }
    }
}