using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionaisSaudeController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public ProfissionaisSaudeController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfissionalSaude>>> GetProfissionaisSaude()
        {
            return await _context.ProfissionaisSaude
                .Include(p => p.Especialidade)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfissionalSaude>> GetProfissionalSaude(Guid id)
        {
            var profissional = await _context.ProfissionaisSaude
                .Include(p => p.Especialidade)
                .Include(p => p.Atendimentos)
                .Include(p => p.Prescricoes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (profissional == null)
                return NotFound();

            return profissional;
        }

        [HttpPost]
        public async Task<ActionResult<ProfissionalSaude>> PostProfissionalSaude(ProfissionalSaude profissional)
        {
            profissional.Id = Guid.NewGuid();
            _context.ProfissionaisSaude.Add(profissional);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfissionalSaude", new { id = profissional.Id }, profissional);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfissionalSaude(Guid id, ProfissionalSaude profissional)
        {
            if (id != profissional.Id)
                return BadRequest();

            _context.Entry(profissional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfissionalSaudeExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfissionalSaude(Guid id)
        {
            var profissional = await _context.ProfissionaisSaude.FindAsync(id);
            if (profissional == null)
                return NotFound();

            _context.ProfissionaisSaude.Remove(profissional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfissionalSaudeExists(Guid id)
        {
            return _context.ProfissionaisSaude.Any(e => e.Id == id);
        }
    }

}
