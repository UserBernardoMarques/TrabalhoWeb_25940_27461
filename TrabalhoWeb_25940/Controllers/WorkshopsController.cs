using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkshopsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Workshops
        // Lista todos os workshops (Leitura)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workshop>>> GetWorkshops()
        {
            return await _context.Workshops
                .Include(w => w.Categoria)
                .ToListAsync();
        }

        // GET: api/Workshops/5
        // Detalhes de um workshop específico
        [HttpGet("{id}")]
        public async Task<ActionResult<Workshop>> GetWorkshop(int id)
        {
            var workshop = await _context.Workshops
                .Include(w => w.Categoria)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workshop == null) return NotFound();

            return workshop;
        }

        // POST: api/Workshops
        // Criar um novo workshop (Inserção)
        [HttpPost]
        public async Task<ActionResult<Workshop>> PostWorkshop(Workshop workshop)
        {
            _context.Workshops.Add(workshop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkshop), new { id = workshop.Id }, workshop);
        }

        // PUT: api/Workshops/5
        // Atualizar um workshop (Edição)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshop(int id, Workshop workshop)
        {
            if (id != workshop.Id) return BadRequest();

            _context.Entry(workshop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Workshops.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/Workshops/5
        // Eliminar um workshop (Eliminação)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshop(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null) return NotFound();

            _context.Workshops.Remove(workshop);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}