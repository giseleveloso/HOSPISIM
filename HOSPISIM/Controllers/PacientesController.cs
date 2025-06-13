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
        public async Task<ActionResult<IEnumerable<object>>> GetPacientes()
        {
            var pacientes = await _context.Pacientes
                .Include(p => p.Prontuarios)
                .Include(p => p.Internacoes)
                .Select(p => new
                {
                    p.Id,
                    p.NomeCompleto,
                    p.CPF,
                    p.DataNascimento,
                    p.Sexo,
                    p.TipoSanguineo,
                    p.Telefone,
                    p.Email,
                    p.PossuiPlanoSaude,
                    QuantidadeProntuarios = p.Prontuarios.Count,
                    QuantidadeInternacoes = p.Internacoes.Count,
                    StatusInternacao = p.Internacoes.Any(i => i.StatusInternacao == "Ativa") ? "Internado" : "Não Internado"
                })
                .ToListAsync();

            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPaciente(Guid id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Prontuarios)
                .ThenInclude(pr => pr.Atendimentos)
                .Include(p => p.Internacoes)
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.NomeCompleto,
                    p.CPF,
                    p.DataNascimento,
                    p.Sexo,
                    p.TipoSanguineo,
                    p.Telefone,
                    p.Email,
                    p.EnderecoCompleto,
                    p.NumeroCartaoSUS,
                    p.EstadoCivil,
                    p.PossuiPlanoSaude,
                    Idade = DateTime.Now.Year - p.DataNascimento.Year,
                    Prontuarios = p.Prontuarios.Select(pr => new
                    {
                        pr.Id,
                        pr.Numero,
                        pr.DataAbertura,
                        pr.ObservacoesGerais,
                        QuantidadeAtendimentos = pr.Atendimentos.Count,
                        UltimoAtendimento = pr.Atendimentos
                            .OrderByDescending(a => a.DataHora)
                            .Select(a => new
                            {
                                a.Id,
                                a.DataHora,
                                a.Tipo,
                                a.Status,
                                a.Local
                            }).FirstOrDefault()
                    }).ToList(),
                    Internacoes = p.Internacoes.Select(i => new
                    {
                        i.Id,
                        i.DataEntrada,
                        i.PrevisaoAlta,
                        i.MotivoInternacao,
                        i.Leito,
                        i.Quarto,
                        i.Setor,
                        i.StatusInternacao
                    }).ToList(),
                    InternacaoAtiva = p.Internacoes.Where(i => i.StatusInternacao == "Ativa").Select(i => new
                    {
                        i.Id,
                        i.DataEntrada,
                        i.PrevisaoAlta,
                        i.MotivoInternacao,
                        i.Leito,
                        i.Quarto,
                        i.Setor
                    }).FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (paciente == null)
                return NotFound();

            return Ok(paciente);
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
