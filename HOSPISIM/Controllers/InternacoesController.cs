using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternacoesController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public InternacoesController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetInternacoes()
        {
            var internacoes = await _context.Internacoes
                .Include(i => i.Paciente)
                .Include(i => i.Atendimento)
                .Select(i => new
                {
                    i.Id,
                    i.DataEntrada,
                    i.PrevisaoAlta,
                    i.MotivoInternacao,
                    i.Leito,
                    i.Quarto,
                    i.Setor,
                    i.StatusInternacao,
                    Paciente = new
                    {
                        i.Paciente.Id,
                        i.Paciente.NomeCompleto,
                        i.Paciente.CPF,
                        i.Paciente.DataNascimento
                    },
                    Atendimento = new
                    {
                        i.Atendimento.Id,
                        i.Atendimento.DataHora,
                        i.Atendimento.Tipo,
                        i.Atendimento.Status
                    }
                })
                .ToListAsync();

            return Ok(internacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetInternacao(Guid id)
        {
            var internacao = await _context.Internacoes
                .Include(i => i.Paciente)
                .Include(i => i.Atendimento)
                .Include(i => i.AltaHospitalar)
                .Where(i => i.Id == id)
                .Select(i => new
                {
                    i.Id,
                    i.DataEntrada,
                    i.PrevisaoAlta,
                    i.MotivoInternacao,
                    i.Leito,
                    i.Quarto,
                    i.Setor,
                    i.PlanoSaudeUtilizado,
                    i.ObservacoesClinicas,
                    i.StatusInternacao,
                    Paciente = new
                    {
                        i.Paciente.Id,
                        i.Paciente.NomeCompleto,
                        i.Paciente.CPF,
                        i.Paciente.DataNascimento,
                        i.Paciente.Sexo,
                        i.Paciente.TipoSanguineo,
                        i.Paciente.Telefone,
                        i.Paciente.Email
                    },
                    Atendimento = new
                    {
                        i.Atendimento.Id,
                        i.Atendimento.DataHora,
                        i.Atendimento.Tipo,
                        i.Atendimento.Status,
                        i.Atendimento.Local
                    },
                    AltaHospitalar = i.AltaHospitalar != null ? new
                    {
                        i.AltaHospitalar.Id,
                        i.AltaHospitalar.DataAlta,
                        i.AltaHospitalar.CondicaoPaciente,
                        i.AltaHospitalar.InstrucoesPosAlta
                    } : null
                })
                .FirstOrDefaultAsync();

            if (internacao == null)
                return NotFound();

            return Ok(internacao);
        }


        [HttpPost]
        public async Task<ActionResult<Internacao>> PostInternacao(Internacao internacao)
        {
            internacao.Id = Guid.NewGuid();
            _context.Internacoes.Add(internacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInternacao", new { id = internacao.Id }, internacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInternacao(Guid id, Internacao internacao)
        {
            if (id != internacao.Id)
                return BadRequest();

            _context.Entry(internacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternacaoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInternacao(Guid id)
        {
            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao == null)
                return NotFound();

            _context.Internacoes.Remove(internacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InternacaoExists(Guid id)
        {
            return _context.Internacoes.Any(e => e.Id == id);
        }
    }

}
