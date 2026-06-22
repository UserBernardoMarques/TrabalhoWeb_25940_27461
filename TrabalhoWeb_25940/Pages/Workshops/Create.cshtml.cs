using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // A MAGIA ESTÁ AQUI: Mudámos o último parâmetro de "Id" para "Nome"
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Workshop Workshop { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Workshops == null || Workshop == null)
            {
                return Page();
            }

            _context.Workshops.Add(Workshop);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}