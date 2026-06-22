using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class InscreverModel : PageModel
    {
        private readonly AppDbContext _context;

        public InscreverModel(AppDbContext context)
        {
            _context = context;
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

            // Tenta encontrar a conta do utilizador pelo email
            var participante = await _context.Participantes
                .FirstOrDefaultAsync(p => p.Email == EmailConfirmacao);

            if (participante == null)
            {
                ModelState.AddModelError("EmailConfirmacao", "Conta não encontrada.");
                Workshop = workshopAtual!;
                return Page();
            }

            // Cria um pedido pendente na nova tabela de Inscrições para o Admin ver!
            var novoPedido = new Inscricao
            {
                ParticipanteId = participante.Id,
                WorkshopId = workshopAtual!.Id,
                DataPedido = DateTime.Now,
                Aprovada = false // Fica a falso até carregares em "Aceitar"
            };

            _context.Inscricoes.Add(novoPedido);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}