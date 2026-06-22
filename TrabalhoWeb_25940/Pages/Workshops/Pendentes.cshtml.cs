using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class PendentesModel : PageModel
    {
        private readonly AppDbContext _context;

        public PendentesModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Workshop> WorkshopPendente { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Vai buscar à Base de Dados APENAS os workshops com Aprovado = false
            WorkshopPendente = await _context.Workshops
                .Include(w => w.Categoria)
                .Where(w => w.Aprovado == false)
                .ToListAsync();
        }

        // Lógica do botão APROVAR
        public async Task<IActionResult> OnPostAprovarAsync(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop != null)
            {
                workshop.Aprovado = true; // Muda o estado para aprovado!
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // Lógica do botão REJEITAR
        public async Task<IActionResult> OnPostRejeitarAsync(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop != null)
            {
                _context.Workshops.Remove(workshop); // Apaga a proposta
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}