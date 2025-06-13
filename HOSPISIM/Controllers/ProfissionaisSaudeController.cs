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
        public async Task<ActionResult<IEnumerable<object>>> GetProfissionaisSaude()
        {
            var profissionais = await _context.ProfissionaisSaude
                .Include(p => p.Especialidade)
                .Select(p => new
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
                    p.Ativo,
                    Especialidade = new
                    {
                        p.Especialidade.Id,
                        p.Especialidade.Nome
                    }
                })
                .ToListAsync();

            return Ok(profissionais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProfissionalSaude(Guid id)
        {
            var profissional = await _context.ProfissionaisSaude
                .Include(p => p.Especialidade)
                .Include(p => p.Atendimentos)
                .Include(p => p.Prescricoes)
                .Where(p => p.Id == id)
                .Select(p => new
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
                    p.Ativo,
                    Especialidade = new
                    {
                        p.Especialidade.Id,
                        p.Especialidade.Nome
                    },
                    QuantidadeAtendimentos = p.Atendimentos.Count,
                    QuantidadePrescricoes = p.Prescricoes.Count,
                    UltimosAtendimentos = p.Atendimentos
                        .OrderByDescending(a => a.DataHora)
                        .Take(5)
                        .Select(a => new
                        {
                            a.Id,
                            a.DataHora,
                            a.Tipo,
                            a.Status
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (profissional == null)
                return NotFound();

            return Ok(profissional);
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
