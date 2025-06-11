using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescricoesController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public PrescricoesController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescricao>>> GetPrescricoes()
        {
            return await _context.Prescricoes
                .Include(p => p.Atendimento)
                .Include(p => p.Profissional)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prescricao>> GetPrescricao(Guid id)
        {
            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento)
                .Include(p => p.Profissional)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescricao == null)
                return NotFound();

            return prescricao;
        }

        [HttpPost]
        public async Task<ActionResult<Prescricao>> PostPrescricao(Prescricao prescricao)
        {
            prescricao.Id = Guid.NewGuid();
            _context.Prescricoes.Add(prescricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrescricao", new { id = prescricao.Id }, prescricao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescricao(Guid id, Prescricao prescricao)
        {
            if (id != prescricao.Id)
                return BadRequest();

            _context.Entry(prescricao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescricaoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescricao(Guid id)
        {
            var prescricao = await _context.Prescricoes.FindAsync(id);
            if (prescricao == null)
                return NotFound();

            _context.Prescricoes.Remove(prescricao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrescricaoExists(Guid id)
        {
            return _context.Prescricoes.Any(e => e.Id == id);
        }
    }
}
