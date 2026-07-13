using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    [Authorize] // <-- O CADEADO DE SEGURANÇA ESTÁ AQUI
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Se o teu workshop tem uma categoria, isto preenche a dropdown list
            // Descomenta a linha abaixo se usares Categorias no teu projeto:
            // ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Workshop Workshop { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Workshops.Add(Workshop);
            await _context.SaveChangesAsync();

            // NOTA: Se tinhas aqui o código do SignalR para o sininho, volta a colá-lo aqui!

            return RedirectToPage("./Index");
        }
    }
}