using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Participantes
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Participante> Participante { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Mostra APENAS as contas não aprovadas (e esconde a tua conta de admin!)
            Participante = await _context.Participantes
                .Where(p => p.Aprovado == false && p.Email != "admin@gather.io")
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAprovarAsync(int id)
        {
            var conta = await _context.Participantes.FindAsync(id);
            if (conta != null)
            {
                conta.Aprovado = true; // Conta aceite!
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejeitarAsync(int id)
        {
            var conta = await _context.Participantes.FindAsync(id);
            if (conta != null)
            {
                _context.Participantes.Remove(conta); // Conta apagada!
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}