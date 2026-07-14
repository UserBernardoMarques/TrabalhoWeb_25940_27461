using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR; // Adicionado
using TrabalhoWeb_25940.Hubs; // Adicionado
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class InscricoesPendentesModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<WorkshopHub> _hubContext; // Adicionado

        public InscricoesPendentesModel(AppDbContext context, IHubContext<WorkshopHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IList<Inscricao> Inscricoes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Inscricoes = await _context.Inscricoes
                .Include(i => i.Participante)
                .Include(i => i.Workshop)
                .Where(i => i.Aprovada == false)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAprovarAsync(int id)
        {
            var inscricao = await _context.Inscricoes
                .Include(i => i.Workshop)
                .Include(i => i.Participante)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inscricao != null)
            {
                inscricao.Aprovada = true;
                await _context.SaveChangesAsync();

                // 📢 NOTIFICAÇÃO SIGNALR: Avisa o Aluno que o seu pedido foi aprovado!
                try
                {
                    await _hubContext.Clients.All.SendAsync("ReceberNotificacao", $"A tua inscrição no workshop '{inscricao.Workshop.Titulo}' foi aprovada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro SignalR: " + ex.Message);
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejeitarAsync(int id)
        {
            var inscricao = await _context.Inscricoes
                .Include(i => i.Workshop)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inscricao != null)
            {
                string tituloWorkshop = inscricao.Workshop.Titulo;
                _context.Inscricoes.Remove(inscricao);
                await _context.SaveChangesAsync();

                // 📢 NOTIFICAÇÃO SIGNALR: Avisa que foi rejeitado
                try
                {
                    await _hubContext.Clients.All.SendAsync("ReceberNotificacao", $"O pedido de inscrição para o workshop '{tituloWorkshop}' foi rejeitado.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro SignalR: " + ex.Message);
                }
            }
            return RedirectToPage();
        }
    }
}