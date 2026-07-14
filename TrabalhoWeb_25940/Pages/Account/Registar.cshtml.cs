using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR; // IMPORTANTE para o SignalR
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Hubs; // IMPORTANTE para ligar ao teu Hub
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Account
{
    public class RegistarModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<WorkshopHub> _hubContext; // O nosso "gatilho" do sino

        // Construtor atualizado para receber o Hub
        public RegistarModel(AppDbContext context, IHubContext<WorkshopHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            public string Nome { get; set; } = "";
            public int Idade { get; set; }
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
            public bool IsAluno { get; set; }
            public bool IsFormador { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (_context.Participantes.Any(p => p.Email == Input.Email))
            {
                ModelState.AddModelError("", "Email já registado.");
                return Page();
            }

            var novoParticipante = new Participante
            {
                Nome = Input.Nome,
                Email = Input.Email,
                Idade = Input.Idade,
                Password = Input.Password,
                IsAluno = Input.IsAluno,
                IsFormador = Input.IsFormador
            };

            _context.Participantes.Add(novoParticipante);
            await _context.SaveChangesAsync();

            // 📢 NOTIFICAÇÃO SIGNALR: Avisa o Administrador que há um novo registo para aprovar!
            try
            {
                await _hubContext.Clients.All.SendAsync("ReceberNotificacao", $"Novo pedido de conta recebido de: {novoParticipante.Nome}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro SignalR: " + ex.Message);
            }

            return RedirectToPage("/Account/Login");
        }
    }
}