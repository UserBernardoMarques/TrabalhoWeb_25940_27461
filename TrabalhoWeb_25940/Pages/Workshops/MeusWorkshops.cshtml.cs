using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class MeusWorkshopsModel : PageModel
    {
        private readonly AppDbContext _context;

        public MeusWorkshopsModel(AppDbContext context)
        {
            _context = context;
        }

        // Uma pequena "caixa" para transportar o Workshop e os respetivos Alunos ao mesmo tempo
        public class WorkshopInfo
        {
            public Workshop Workshop { get; set; } = default!;
            public List<Inscricao> InscricoesAprovadas { get; set; } = new List<Inscricao>();
        }

        public List<WorkshopInfo> MeusWorkshops { get; set; } = new List<WorkshopInfo>();

        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Descobre o ID do Formador que tem o login feito
            var idClaim = User.FindFirstValue("ParticipanteId");
            if (idClaim == null) return RedirectToPage("/Account/Login");

            int meuId = int.Parse(idClaim);

            // 2. Vai buscar todos os workshops propostos por ele
            var workshops = await _context.Workshops
                .Where(w => w.FormadorId == meuId)
                .ToListAsync();

            // 3. Para cada workshop, pesquisa quem já está inscrito E aprovado
            foreach (var w in workshops)
            {
                var inscricoes = await _context.Inscricoes
                    .Include(i => i.Participante)
                    .Where(i => i.WorkshopId == w.Id && i.Aprovada == true)
                    .ToListAsync();

                MeusWorkshops.Add(new WorkshopInfo
                {
                    Workshop = w,
                    InscricoesAprovadas = inscricoes
                });
            }

            return Page();
        }
    }
}