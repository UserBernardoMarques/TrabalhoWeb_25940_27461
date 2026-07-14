using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR; // Adicionado
using TrabalhoWeb_25940.Hubs; // Adicionado
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class InscreverModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<WorkshopHub> _hubContext; // Adicionado

        public InscreverModel(AppDbContext context, IHubContext<WorkshopHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Workshop Workshop { get; set; } = default!;

        [BindProperty]
        public string EmailConfirmacao { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var workshop = await _context.Workshops.FirstOrDefaultAsync(m => m.Id == id);
            if (workshop == null) return NotFound();

            Workshop = workshop;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var workshopAtual = await _context.Workshops.FindAsync(Workshop.Id);

            var participante = await _context.Participantes
                .FirstOrDefaultAsync(p => p.Email == EmailConfirmacao);

            if (participante == null)
            {
                ModelState.AddModelError("EmailConfirmacao", "Conta não encontrada.");
                Workshop = workshopAtual!;
                return Page();
            }

            var novoPedido = new Inscricao
            {
                ParticipanteId = participante.Id,
                WorkshopId = workshopAtual!.Id,
                DataPedido = DateTime.Now,
                Aprovada = false
            };

            _context.Inscricoes.Add(novoPedido);
            await _context.SaveChangesAsync();

            // 📢 NOTIFICAÇÃO SIGNALR: Avisa o Administrador que há uma nova inscrição pendente!
            try
            {
                await _hubContext.Clients.All.SendAsync("ReceberNotificacao", $"Nova inscrição pendente para o workshop '{workshopAtual.Titulo}'!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro SignalR: " + ex.Message);
            }

            return RedirectToPage("/Index");
        }
    }
}