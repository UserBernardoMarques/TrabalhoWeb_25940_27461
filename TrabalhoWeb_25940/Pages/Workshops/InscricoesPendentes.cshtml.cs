using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class InscricoesPendentesModel : PageModel
    {
        private readonly AppDbContext _context;

        public InscricoesPendentesModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Inscricao> Inscricoes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Vai buscar apenas as inscrições que ainda não foram aprovadas
            Inscricoes = await _context.Inscricoes
                .Include(i => i.Participante)
                .Include(i => i.Workshop)
                .Where(i => i.Aprovada == false)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAprovarAsync(int id)
        {
            var inscricao = await _context.Inscricoes.FindAsync(id);
            if (inscricao != null)
            {
                inscricao.Aprovada = true; // Aceita o aluno no workshop!
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejeitarAsync(int id)
        {
            var inscricao = await _context.Inscricoes.FindAsync(id);
            if (inscricao != null)
            {
                _context.Inscricoes.Remove(inscricao); // Apaga o pedido
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}