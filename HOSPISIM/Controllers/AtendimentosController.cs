using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentosController : ControllerBase
    {
        private readonly HospiSimDbContext _context;

        public AtendimentosController(HospiSimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atendimento>>> GetAtendimentos()
        {
            return await _context.Atendimentos
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Include(a => a.Prontuario)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Atendimento>> GetAtendimento(Guid id)
        {
            var atendimento = await _context.Atendimentos
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Include(a => a.Prontuario)
                .Include(a => a.Prescricoes)
                .Include(a => a.Exames)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (atendimento == null)
            {
                return NotFound();
            }

            return atendimento;
        }

        [HttpPost]
        public async Task<ActionResult<Atendimento>> PostAtendimento(Atendimento atendimento)
        {
            atendimento.Id = Guid.NewGuid();
            _context.Atendimentos.Add(atendimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAtendimento", new { id = atendimento.Id }, atendimento);
        }
    }
}
