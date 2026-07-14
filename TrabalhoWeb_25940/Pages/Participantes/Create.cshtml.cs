using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR; // IMPORTANTE para o SignalR
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Hubs; // Garante que este namespace aponta para a pasta do teu WorkshopHub
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Participantes
{
    public class CreateModel : PageModel
    {
        private readonly TrabalhoWeb_25940.Data.AppDbContext _context;
        private readonly IHubContext<WorkshopHub> _hubContext; // Injeção do SignalR

        // Construtor atualizado para receber o Hub
        public CreateModel(TrabalhoWeb_25940.Data.AppDbContext context, IHubContext<WorkshopHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Participante Participante { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Participantes.Add(Participante);
            await _context.SaveChangesAsync();

            // NOTIFICAÇÃO SIGNALR: Toca o sino a avisar o Admin que há uma nova proposta/pedido!
            try
            {
                await _hubContext.Clients.All.SendAsync("ReceberNotificacao", "Novo pedido de participação recebido na plataforma!");
            }
            catch (Exception ex)
            {
                // Silencioso se houver algum problema local na ligação
                Console.WriteLine("Erro SignalR: " + ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}