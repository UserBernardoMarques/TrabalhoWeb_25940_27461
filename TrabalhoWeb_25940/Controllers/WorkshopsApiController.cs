using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoWeb_25940.Data;

namespace TrabalhoWeb_25940.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkshopsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkshopsApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkshopsApi
        // Devolve a lista de todos os workshops aprovados em formato JSON
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetWorkshops()
        {
            var workshops = await _context.Workshops
                .Include(w => w.Categoria)
                .Where(w => w.Aprovado == true)
                .Select(w => new
                {
                    w.Id,
                    w.Titulo,
                    w.Descricao,
                    w.Data,
                    Categoria = w.Categoria != null ? w.Categoria.Nome : "Sem Categoria"
                })
                .ToListAsync();

            return Ok(workshops);
        }

        // GET: api/WorkshopsApi/5
        // Devolve apenas um workshop específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetWorkshop(int id)
        {
            var workshop = await _context.Workshops
                .Include(w => w.Categoria)
                .Where(w => w.Id == id)
                .Select(w => new
                {
                    w.Id,
                    w.Titulo,
                    w.Descricao,
                    w.Data,
                    Categoria = w.Categoria != null ? w.Categoria.Nome : "Sem Categoria"
                })
                .FirstOrDefaultAsync();

            if (workshop == null)
            {
                return NotFound(new { mensagem = "Workshop não encontrado." });
            }

            return Ok(workshop);
        }
    }
}