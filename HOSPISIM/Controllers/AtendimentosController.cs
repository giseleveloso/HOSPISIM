using HOSPISIM.Models;
using HOSPISIM.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AtendimentosController : ControllerBase
{
    private readonly HospiSimDbContext _context;

    public AtendimentosController(HospiSimDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAtendimentos()
    {
        var atendimentos = await _context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Profissional)
            .Include(a => a.Prontuario)
            .Select(a => new
            {
                a.Id,
                a.DataHora,
                a.Tipo,
                a.Status,
                a.Local,
                Paciente = new
                {
                    a.Paciente.Id,
                    a.Paciente.NomeCompleto,
                    a.Paciente.CPF,
                    a.Paciente.DataNascimento,
                    a.Paciente.Sexo,
                    a.Paciente.TipoSanguineo,
                    a.Paciente.Telefone,
                    a.Paciente.Email
                },
                Profissional = new
                {
                    a.Profissional.Id,
                    a.Profissional.NomeCompleto,
                    a.Profissional.RegistroConselho,
                    a.Profissional.TipoRegistro,
                    a.Profissional.Turno
                },
                Prontuario = new
                {
                    a.Prontuario.Id,
                    a.Prontuario.Numero,
                    a.Prontuario.DataAbertura
                }
            })
            .ToListAsync();

        return Ok(atendimentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetAtendimento(Guid id)
    {
        var atendimento = await _context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Profissional).ThenInclude(p => p.Especialidade)
            .Include(a => a.Prontuario)
            .Include(a => a.Prescricoes)
            .Include(a => a.Exames)
            .Include(a => a.Internacao)
            .Where(a => a.Id == id)
            .Select(a => new
            {
                a.Id,
                a.DataHora,
                a.Tipo,
                a.Status,
                a.Local,
                Paciente = new
                {
                    a.Paciente.Id,
                    a.Paciente.NomeCompleto,
                    a.Paciente.CPF,
                    a.Paciente.DataNascimento,
                    a.Paciente.Sexo,
                    a.Paciente.TipoSanguineo,
                    a.Paciente.Telefone,
                    a.Paciente.Email,
                    a.Paciente.EnderecoCompleto,
                    a.Paciente.NumeroCartaoSUS,
                    a.Paciente.EstadoCivil,
                    a.Paciente.PossuiPlanoSaude
                },
                Profissional = new
                {
                    a.Profissional.Id,
                    a.Profissional.NomeCompleto,
                    a.Profissional.RegistroConselho,
                    a.Profissional.TipoRegistro,
                    a.Profissional.Turno,
                    a.Profissional.CargaHorariaSemanal,
                    Especialidade = new
                    {
                        a.Profissional.Especialidade.Id,
                        a.Profissional.Especialidade.Nome
                    }
                },
                Prontuario = new
                {
                    a.Prontuario.Id,
                    a.Prontuario.Numero,
                    a.Prontuario.DataAbertura,
                    a.Prontuario.ObservacoesGerais
                },
                Prescricoes = a.Prescricoes.Select(p => new
                {
                    p.Id,
                    p.Medicamento,
                    p.Dosagem,
                    p.Frequencia,
                    p.ViaAdministracao,
                    p.DataInicio,
                    p.DataFim,
                    p.StatusPrescricao
                }),
                Exames = a.Exames.Select(e => new
                {
                    e.Id,
                    e.Tipo,
                    e.DataSolicitacao,
                    e.DataRealizacao,
                    e.Resultado
                }),
                Internacao = a.Internacao != null ? new
                {
                    a.Internacao.Id,
                    a.Internacao.DataEntrada,
                    a.Internacao.PrevisaoAlta,
                    a.Internacao.MotivoInternacao,
                    a.Internacao.Leito,
                    a.Internacao.Quarto,
                    a.Internacao.Setor,
                    a.Internacao.StatusInternacao
                } : null
            })
            .FirstOrDefaultAsync();

        if (atendimento == null)
        {
            return NotFound();
        }

        return Ok(atendimento);
    }

    [HttpPost]
    public async Task<ActionResult<Atendimento>> PostAtendimento(Atendimento atendimento)
    {
        _context.Atendimentos.Add(atendimento);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAtendimento", new { id = atendimento.Id }, atendimento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAtendimento(Guid id, Atendimento atendimento)
    {
        if (id != atendimento.Id)
        {
            return BadRequest();
        }

        _context.Entry(atendimento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AtendimentoExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAtendimento(Guid id)
    {
        var atendimento = await _context.Atendimentos.FindAsync(id);
        if (atendimento == null)
        {
            return NotFound();
        }

        _context.Atendimentos.Remove(atendimento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AtendimentoExists(Guid id)
    {
        return _context.Atendimentos.Any(e => e.Id == id);
    }
}