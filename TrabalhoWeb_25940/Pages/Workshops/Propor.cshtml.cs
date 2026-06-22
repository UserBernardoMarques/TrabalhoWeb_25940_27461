using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class ProporModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProporModel(AppDbContext context)
        {
            _context = context;
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
            // Apanha o ID do Formador que está a propor o workshop
            var idClaim = User.FindFirstValue("ParticipanteId");
            if (idClaim != null)
            {
                Workshop.FormadorId = int.Parse(idClaim);
            }

            Workshop.Aprovado = false; // Vai para a lista de pendentes do Admin

            _context.Workshops.Add(Workshop);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}