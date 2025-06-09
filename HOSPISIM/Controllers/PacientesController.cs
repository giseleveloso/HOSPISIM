using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOSPISIM.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class PacientesController : ControllerBase
        {
            private readonly HospiSimDbContext _context;

            public PacientesController(HospiSimDbContext context)
            {
                _context = context;
            }

            // GET: api/Pacientes
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
            {
                return await _context.Pacientes.ToListAsync();
            }

            // GET: api/Pacientes/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Paciente>> GetPaciente(Guid id)
            {
                var paciente = await _context.Pacientes.FindAsync(id);

                if (paciente == null)
                {
                    return NotFound();
                }

                return paciente;
            }

            // PUT: api/Pacientes/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutPaciente(Guid id, Paciente paciente)
            {
                if (id != paciente.Id)
                {
                    return BadRequest();
                }

                _context.Entry(paciente).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // POST: api/Pacientes
            [HttpPost]
            public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
            {
                paciente.Id = Guid.NewGuid();
                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPaciente", new { id = paciente.Id }, paciente);
            }

            // DELETE: api/Pacientes/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePaciente(Guid id)
            {
                var paciente = await _context.Pacientes.FindAsync(id);
                if (paciente == null)
                {
                    return NotFound();
                }

                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool PacienteExists(Guid id)
            {
                return _context.Pacientes.Any(e => e.Id == id);
            }
        }
    }
