using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Participantes
{
    public class AtivosModel : PageModel
    {
        private readonly AppDbContext _context;

        public AtivosModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Participante> Alunos { get; set; } = new List<Participante>();
        public IList<Participante> Formadores { get; set; } = new List<Participante>();

        public async Task OnGetAsync()
        {
            // Puxa todos os utilizadores que JÁ FORAM aprovados, 
            // e remove o teu admin da lista para não o apagares sem querer!
            var todosAprovados = await _context.Participantes
                .Where(p => p.Aprovado == true && p.Email != "admin@gather.io")
                .ToListAsync();

            // Separa os dados em duas listas
            Alunos = todosAprovados.Where(p => p.IsAluno).ToList();
            Formadores = todosAprovados.Where(p => p.IsFormador).ToList();

            // Nota: Se um utilizador for Aluno E Formador, vai aparecer nas duas tabelas!
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var conta = await _context.Participantes.FindAsync(id);

            if (conta != null)
            {
                // 1. Limpa todas as inscrições pendentes ou aceites deste utilizador
                var inscricoesAssociadas = _context.Inscricoes.Where(i => i.ParticipanteId == id);
                _context.Inscricoes.RemoveRange(inscricoesAssociadas);

                // 2. Apaga definitivamente a conta do sistema
                _context.Participantes.Remove(conta);

                await _context.SaveChangesAsync();
            }

            // Recarrega a página para mostrar a tabela atualizada
            return RedirectToPage();
        }
    }
}