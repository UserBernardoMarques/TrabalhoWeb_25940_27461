using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR; // Adicionado
using TrabalhoWeb_25940.Hubs; // Adicionado
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class ProporModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<WorkshopHub> _hubContext; // Adicionado

        public ProporModel(AppDbContext context, IHubContext<WorkshopHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Workshop Workshop { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var idClaim = User.FindFirstValue("ParticipanteId");
            if (idClaim != null)
            {
                Workshop.FormadorId = int.Parse(idClaim);
            }

            Workshop.Aprovado = false;

            _context.Workshops.Add(Workshop);
            await _context.SaveChangesAsync();

            // 📢 NOTIFICAÇÃO SIGNALR: Avisa o Administrador que há uma nova proposta de formação!
            try
            {
                await _hubContext.Clients.All.SendAsync("ReceberNotificacao", $"Nova proposta de workshop recebida: '{Workshop.Titulo}'!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro SignalR: " + ex.Message);
            }

            return RedirectToPage("/Index");
        }
    }
}