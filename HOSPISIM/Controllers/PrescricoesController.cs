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
        public async Task<ActionResult<IEnumerable<object>>> GetPrescricoes()
        {
            var prescricoes = await _context.Prescricoes
                .Include(p => p.Atendimento)
                .Include(p => p.Profissional)
                .Select(p => new
                {
                    p.Id,
                    p.Medicamento,
                    p.Dosagem,
                    p.Frequencia,
                    p.ViaAdministracao,
                    p.DataInicio,
                    p.DataFim,
                    p.StatusPrescricao,
                    Atendimento = new
                    {
                        p.Atendimento.Id,
                        p.Atendimento.DataHora,
                        p.Atendimento.Tipo,
                        p.Atendimento.Status
                    },
                    Profissional = new
                    {
                        p.Profissional.Id,
                        p.Profissional.NomeCompleto,
                        p.Profissional.RegistroConselho,
                        p.Profissional.TipoRegistro
                    }
                })
                .ToListAsync();

            return Ok(prescricoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPrescricao(Guid id)
        {
            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento)
                .Include(p => p.Profissional)
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Medicamento,
                    p.Dosagem,
                    p.Frequencia,
                    p.ViaAdministracao,
                    p.DataInicio,
                    p.DataFim,
                    p.Observacoes,
                    p.StatusPrescricao,
                    p.ReacoesAdversas,
                    Atendimento = new
                    {
                        p.Atendimento.Id,
                        p.Atendimento.DataHora,
                        p.Atendimento.Tipo,
                        p.Atendimento.Status,
                        p.Atendimento.Local
                    },
                    Profissional = new
                    {
                        p.Profissional.Id,
                        p.Profissional.NomeCompleto,
                        p.Profissional.RegistroConselho,
                        p.Profissional.TipoRegistro,
                        p.Profissional.Turno
                    }
                })
                .FirstOrDefaultAsync();

            if (prescricao == null)
                return NotFound();

            return Ok(prescricao);
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
