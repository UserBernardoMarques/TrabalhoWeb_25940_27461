using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class ExplorarModel : PageModel
    {
        private readonly AppDbContext _context;

        public ExplorarModel(AppDbContext context)
        {
            _context = context;
        }

        // Nota: O nome da lista pode ser 'Workshop' ou 'Workshops' dependendo do teu HTML.
        // Se no teu HTML estiver '@foreach (var item in Model.Workshop)', deixa ficar assim:
        public IList<Workshop> Workshop { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // A CORREÇÃO ESTÁ AQUI: O .Where(w => w.Aprovado == true) filtra a lista!
            Workshop = await _context.Workshops
                .Include(w => w.Categoria)
                .Where(w => w.Aprovado == true)
                .OrderBy(w => w.Data) // Bónus: Mostra os mais recentes primeiro
                .ToListAsync();
        }
    }
}