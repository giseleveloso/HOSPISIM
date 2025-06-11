using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamesController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public ExamesController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exame>>> GetExames()
        {
            return await _context.Exames
                .Include(e => e.Atendimento)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exame>> GetExame(Guid id)
        {
            var exame = await _context.Exames
                .Include(e => e.Atendimento)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exame == null)
                return NotFound();

            return exame;
        }

        [HttpPost]
        public async Task<ActionResult<Exame>> PostExame(Exame exame)
        {
            exame.Id = Guid.NewGuid();
            _context.Exames.Add(exame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExame", new { id = exame.Id }, exame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExame(Guid id, Exame exame)
        {
            if (id != exame.Id)
                return BadRequest();

            _context.Entry(exame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExameExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExame(Guid id)
        {
            var exame = await _context.Exames.FindAsync(id);
            if (exame == null)
                return NotFound();

            _context.Exames.Remove(exame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExameExists(Guid id)
        {
            return _context.Exames.Any(e => e.Id == id);
        }
    }

}
