using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR; // Adicionado
using TrabalhoWeb_25940.Hubs; // Adicionado
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class PendentesModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<WorkshopHub> _hubContext; // Adicionado

        public PendentesModel(AppDbContext context, IHubContext<WorkshopHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IList<Workshop> WorkshopPendente { get; set; } = default!;

        public async Task OnGetAsync()
        {
            WorkshopPendente = await _context.Workshops
                .Include(w => w.Categoria)
                .Where(w => w.Aprovado == false)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAprovarAsync(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop != null)
            {
                workshop.Aprovado = true;
                await _context.SaveChangesAsync();

                // 📢 NOTIFICAÇÃO SIGNALR: Avisa todos que a formação foi aprovada!
                try
                {
                    await _hubContext.Clients.All.SendAsync("ReceberNotificacao", $"O workshop '{workshop.Titulo}' foi aprovado e já está disponível!");
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
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop != null)
            {
                string titulo = workshop.Titulo;
                _context.Workshops.Remove(workshop);
                await _context.SaveChangesAsync();

                // 📢 NOTIFICAÇÃO SIGNALR (Opcional): Avisa que a proposta foi removida
                try
                {
                    await _hubContext.Clients.All.SendAsync("ReceberNotificacao", $"A proposta para o workshop '{titulo}' foi rejeitada.");
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