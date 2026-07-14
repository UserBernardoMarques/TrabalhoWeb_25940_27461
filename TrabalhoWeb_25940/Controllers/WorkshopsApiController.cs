using Microsoft.AspNetCore.Authorization; // <-- 1. A Biblioteca de Segurança
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

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
        // PÚBLICO: Qualquer pessoa pode ler a lista (Sem cadeado)
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
        // PÚBLICO: Qualquer pessoa pode ler um workshop específico (Sem cadeado)
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

        // --------------------------------------------------------
        // ZONA DE PERIGO: APENAS UTILIZADORES AUTENTICADOS (LOGIN)
        // --------------------------------------------------------

        // POST: api/WorkshopsApi
        [Authorize] // <-- CADEADO!
        [HttpPost]
        public async Task<ActionResult<object>> PostWorkshop(Workshop workshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Workshops.Add(workshop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkshop), new { id = workshop.Id }, workshop);
        }

        // PUT: api/WorkshopsApi/5
        [Authorize] // <-- CADEADO!
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshop(int id, Workshop workshop)
        {
            if (id != workshop.Id)
            {
                return BadRequest(new { mensagem = "O ID enviado no URL não coincide com o ID do corpo do pedido." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(workshop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkshopExists(id))
                {
                    return NotFound(new { mensagem = "Workshop não encontrado para atualizar." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/WorkshopsApi/5
        [Authorize] // <-- CADEADO!
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshop(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null)
            {
                return NotFound(new { mensagem = "Workshop não encontrado para eliminação." });
            }

            _context.Workshops.Remove(workshop);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Workshop eliminado com sucesso!" });
        }

        private bool WorkshopExists(int id)
        {
            return _context.Workshops.Any(e => e.Id == id);
        }
    }
}