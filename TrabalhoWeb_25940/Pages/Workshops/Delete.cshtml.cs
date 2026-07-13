using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    [Authorize] // <-- O CADEADO DE SEGURANÇA ESTÁ AQUI
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
                _context.Workshops.Remove(Workshop);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}