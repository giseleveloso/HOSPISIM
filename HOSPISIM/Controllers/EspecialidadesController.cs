using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    // Especialidades Controller
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public EspecialidadesController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidade>>> GetEspecialidades()
        {
            return await _context.Especialidades.Include(e => e.Profissionais).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidade>> GetEspecialidade(Guid id)
        {
            var especialidade = await _context.Especialidades
                .Include(e => e.Profissionais)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (especialidade == null)
                return NotFound();

            return especialidade;
        }

        [HttpPost]
        public async Task<ActionResult<Especialidade>> PostEspecialidade(Especialidade especialidade)
        {
            especialidade.Id = Guid.NewGuid();
            _context.Especialidades.Add(especialidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspecialidade", new { id = especialidade.Id }, especialidade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidade(Guid id, Especialidade especialidade)
        {
            if (id != especialidade.Id)
                return BadRequest();

            _context.Entry(especialidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadeExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidade(Guid id)
        {
            var especialidade = await _context.Especialidades.FindAsync(id);
            if (especialidade == null)
                return NotFound();

            _context.Especialidades.Remove(especialidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecialidadeExists(Guid id)
        {
            return _context.Especialidades.Any(e => e.Id == id);
        }
    }
}
