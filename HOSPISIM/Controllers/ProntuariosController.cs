using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProntuariosController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public ProntuariosController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetProntuarios()
        {
            var prontuarios = await _context.Prontuarios
                .Include(p => p.Paciente)
                .Select(p => new
                {
                    p.Id,
                    p.Numero,
                    p.DataAbertura,
                    p.ObservacoesGerais,
                    Paciente = new
                    {
                        p.Paciente.Id,
                        p.Paciente.NomeCompleto,
                        p.Paciente.CPF,
                        p.Paciente.DataNascimento
                    }
                })
                .ToListAsync();

            return Ok(prontuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProntuario(Guid id)
        {
            var prontuario = await _context.Prontuarios
                .Include(p => p.Paciente)
                .Include(p => p.Atendimentos)
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Numero,
                    p.DataAbertura,
                    p.ObservacoesGerais,
                    Paciente = new
                    {
                        p.Paciente.Id,
                        p.Paciente.NomeCompleto,
                        p.Paciente.CPF,
                        p.Paciente.DataNascimento,
                        p.Paciente.Sexo,
                        p.Paciente.TipoSanguineo,
                        p.Paciente.Telefone,
                        p.Paciente.Email
                    },
                    QuantidadeAtendimentos = p.Atendimentos.Count,
                    UltimosAtendimentos = p.Atendimentos
                        .OrderByDescending(a => a.DataHora)
                        .Take(5)
                        .Select(a => new
                        {
                            a.Id,
                            a.DataHora,
                            a.Tipo,
                            a.Status,
                            a.Local
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (prontuario == null)
                return NotFound();

            return Ok(prontuario);
        }

        [HttpPost]
        public async Task<ActionResult<Prontuario>> PostProntuario(Prontuario prontuario)
        {
            prontuario.Id = Guid.NewGuid();
            _context.Prontuarios.Add(prontuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProntuario", new { id = prontuario.Id }, prontuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProntuario(Guid id, Prontuario prontuario)
        {
            if (id != prontuario.Id)
                return BadRequest();

            _context.Entry(prontuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProntuarioExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProntuario(Guid id)
        {
            var prontuario = await _context.Prontuarios.FindAsync(id);
            if (prontuario == null)
                return NotFound();

            _context.Prontuarios.Remove(prontuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProntuarioExists(Guid id)
        {
            return _context.Prontuarios.Any(e => e.Id == id);
        }
    }

}
