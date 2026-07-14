using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR; // IMPORTANTE para o SignalR
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Hubs; // Garante que aponta para o teu Hub
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Participantes
{
    public class EditModel : PageModel
    {
        private readonly TrabalhoWeb_25940.Data.AppDbContext _context;
        private readonly IHubContext<WorkshopHub> _hubContext; // Injeção do SignalR

        // Construtor atualizado para receber o Hub
        public EditModel(TrabalhoWeb_25940.Data.AppDbContext context, IHubContext<WorkshopHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Participante Participante { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FirstOrDefaultAsync(m => m.Id == id);
            if (participante == null)
            {
                return NotFound();
            }
            Participante = participante;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // NOTIFICAÇÃO SIGNALR: Toca o sino a avisar o Utilizador que a sua inscrição foi atualizada!
                try
                {
                    await _hubContext.Clients.All.SendAsync("ReceberNotificacao", "Um pedido de participação foi atualizado ou aprovado pela administração!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro SignalR: " + ex.Message);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(Participante.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.Id == id);
        }
    }
}