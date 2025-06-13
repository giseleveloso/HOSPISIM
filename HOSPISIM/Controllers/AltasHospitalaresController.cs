using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AltasHospitalaresController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public AltasHospitalaresController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAltasHospitalares()
        {
            var altas = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                .Select(a => new
                {
                    a.Id,
                    a.DataAlta,
                    a.CondicaoPaciente,
                    a.InstrucoesPosAlta,
                    Internacao = new
                    {
                        a.Internacao.Id,
                        a.Internacao.DataEntrada,
                        a.Internacao.MotivoInternacao,
                        a.Internacao.Leito,
                        a.Internacao.Quarto,
                        a.Internacao.Setor,
                        a.Internacao.StatusInternacao
                    }
                })
                .ToListAsync();

            return Ok(altas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAltaHospitalar(Guid id)
        {
            var alta = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                .Where(a => a.Id == id)
                .Select(a => new
                {
                    a.Id,
                    a.DataAlta,
                    a.CondicaoPaciente,
                    a.InstrucoesPosAlta,
                    Internacao = new
                    {
                        a.Internacao.Id,
                        a.Internacao.DataEntrada,
                        a.Internacao.PrevisaoAlta,
                        a.Internacao.MotivoInternacao,
                        a.Internacao.Leito,
                        a.Internacao.Quarto,
                        a.Internacao.Setor,
                        a.Internacao.PlanoSaudeUtilizado,
                        a.Internacao.ObservacoesClinicas,
                        a.Internacao.StatusInternacao
                    }
                })
                .FirstOrDefaultAsync();

            if (alta == null)
                return NotFound();

            return Ok(alta);
        }

        [HttpPost]
        public async Task<ActionResult<AltaHospitalar>> PostAltaHospitalar(AltaHospitalar alta)
        {
            alta.Id = Guid.NewGuid();
            _context.AltasHospitalares.Add(alta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAltaHospitalar", new { id = alta.Id }, alta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAltaHospitalar(Guid id, AltaHospitalar alta)
        {
            if (id != alta.Id)
                return BadRequest();

            _context.Entry(alta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AltaHospitalarExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAltaHospitalar(Guid id)
        {
            var alta = await _context.AltasHospitalares.FindAsync(id);
            if (alta == null)
                return NotFound();

            _context.AltasHospitalares.Remove(alta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AltaHospitalarExists(Guid id)
        {
            return _context.AltasHospitalares.Any(e => e.Id == id);
        }
    }
}
