using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Workshop Workshop { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops.FirstOrDefaultAsync(m => m.Id == id);

            if (workshop == null)
            {
                return NotFound();
            }
            else
            {
                Workshop = workshop;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops.FindAsync(id);

            if (workshop != null)
            {
                Workshop = workshop;

                // 1. LIMPEZA: Apaga todas as inscrições associadas a este workshop
                var inscricoesAssociadas = _context.Inscricoes.Where(i => i.WorkshopId == id);
                _context.Inscricoes.RemoveRange(inscricoesAssociadas);

                // 2. APAGA O WORKSHOP: Agora a base de dados já deixa apagar sem dar erro!
                _context.Workshops.Remove(Workshop);

                await _context.SaveChangesAsync();
            }

            // Volta para a lista de gestão após apagar
            return RedirectToPage("./Index");
        }
    }
}