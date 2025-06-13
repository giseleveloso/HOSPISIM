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
        public async Task<ActionResult<IEnumerable<object>>> GetEspecialidades()
        {
            var especialidades = await _context.Especialidades
                .Include(e => e.Profissionais)
                .Select(e => new
                {
                    e.Id,
                    e.Nome,
                    QuantidadeProfissionais = e.Profissionais.Count,
                    Profissionais = e.Profissionais.Select(p => new
                    {
                        p.Id,
                        p.NomeCompleto,
                        p.RegistroConselho,
                        p.TipoRegistro
                    }).ToList()
                })
                .ToListAsync();

            return Ok(especialidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetEspecialidade(Guid id)
        {
            var especialidade = await _context.Especialidades
                .Include(e => e.Profissionais)
                .Where(e => e.Id == id)
                .Select(e => new
                {
                    e.Id,
                    e.Nome,
                    QuantidadeProfissionais = e.Profissionais.Count,
                    Profissionais = e.Profissionais.Select(p => new
                    {
                        p.Id,
                        p.NomeCompleto,
                        p.CPF,
                        p.Email,
                        p.Telefone,
                        p.RegistroConselho,
                        p.TipoRegistro,
                        p.DataAdmissao,
                        p.CargaHorariaSemanal,
                        p.Turno,
                        p.Ativo
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (especialidade == null)
                return NotFound();

            return Ok(especialidade);
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
