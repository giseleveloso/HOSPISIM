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
        public async Task<ActionResult<IEnumerable<object>>> GetExames()
        {
            var exames = await _context.Exames
                .Include(e => e.Atendimento)
                .Select(e => new
                {
                    e.Id,
                    e.Tipo,
                    e.DataSolicitacao,
                    e.DataRealizacao,
                    e.Resultado,
                    Atendimento = new
                    {
                        e.Atendimento.Id,
                        e.Atendimento.DataHora,
                        e.Atendimento.Tipo,
                        e.Atendimento.Status,
                        e.Atendimento.Local
                    }
                })
                .ToListAsync();

            return Ok(exames);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetExame(Guid id)
        {
            var exame = await _context.Exames
                .Include(e => e.Atendimento)
                .ThenInclude(a => a.Paciente)
                .Include(e => e.Atendimento)
                .ThenInclude(a => a.Profissional)
                .Where(e => e.Id == id)
                .Select(e => new
                {
                    e.Id,
                    e.Tipo,
                    e.DataSolicitacao,
                    e.DataRealizacao,
                    e.Resultado,
                    Atendimento = new
                    {
                        e.Atendimento.Id,
                        e.Atendimento.DataHora,
                        e.Atendimento.Tipo,
                        e.Atendimento.Status,
                        e.Atendimento.Local,
                        Paciente = new
                        {
                            e.Atendimento.Paciente.Id,
                            e.Atendimento.Paciente.NomeCompleto,
                            e.Atendimento.Paciente.CPF
                        },
                        Profissional = new
                        {
                            e.Atendimento.Profissional.Id,
                            e.Atendimento.Profissional.NomeCompleto,
                            e.Atendimento.Profissional.RegistroConselho
                        }
                    }
                })
                .FirstOrDefaultAsync();

            if (exame == null)
                return NotFound();

            return Ok(exame);
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
