using HOSPISIM.Models;

namespace HOSPISIM.Persistence
{
    public static class DataSeed
    {
        public static void SeedData(HospiSimDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Especialidades
            if (!context.Especialidades.Any())
            {
                var especialidades = new List<Especialidade>
                {
                    new() { Id = Guid.NewGuid(), Nome = "Cardiologia" },
                    new() { Id = Guid.NewGuid(), Nome = "Pediatria" },
                    new() { Id = Guid.NewGuid(), Nome = "Ortopedia" },
                    new() { Id = Guid.NewGuid(), Nome = "Neurologia" },
                    new() { Id = Guid.NewGuid(), Nome = "Ginecologia" },
                    new() { Id = Guid.NewGuid(), Nome = "Dermatologia" },
                    new() { Id = Guid.NewGuid(), Nome = "Oftalmologia" },
                    new() { Id = Guid.NewGuid(), Nome = "Psiquiatria" },
                    new() { Id = Guid.NewGuid(), Nome = "Urologia" },
                    new() { Id = Guid.NewGuid(), Nome = "Clínica Geral" }
                };

                context.Especialidades.AddRange(especialidades);
                context.SaveChanges();
            }

            // Seed Pacientes
            if (!context.Pacientes.Any())
            {
                var pacientes = new List<Paciente>
                {
                    new() { Id = Guid.NewGuid(), NomeCompleto = "João Silva Santos", CPF = "12345678901", DataNascimento = new DateTime(1985, 5, 15), Sexo = "Masculino", TipoSanguineo = "O+", Telefone = "(11)99999-1111", Email = "joao@email.com", EnderecoCompleto = "Rua A, 123, Centro", NumeroCartaoSUS = "123456789012345", EstadoCivil = "Solteiro", PossuiPlanoSaude = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Maria Oliveira Costa", CPF = "23456789012", DataNascimento = new DateTime(1990, 8, 22), Sexo = "Feminino", TipoSanguineo = "A+", Telefone = "(11)99999-2222", Email = "maria@email.com", EnderecoCompleto = "Rua B, 456, Vila Nova", NumeroCartaoSUS = "234567890123456", EstadoCivil = "Casada", PossuiPlanoSaude = false },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Pedro Souza Lima", CPF = "34567890123", DataNascimento = new DateTime(1978, 12, 3), Sexo = "Masculino", TipoSanguineo = "B-", Telefone = "(11)99999-3333", Email = "pedro@email.com", EnderecoCompleto = "Av. Principal, 789", NumeroCartaoSUS = "345678901234567", EstadoCivil = "Divorciado", PossuiPlanoSaude = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Ana Paula Ferreira", CPF = "45678901234", DataNascimento = new DateTime(1995, 3, 18), Sexo = "Feminino", TipoSanguineo = "AB+", Telefone = "(11)99999-4444", Email = "ana@email.com", EnderecoCompleto = "Rua das Flores, 321", NumeroCartaoSUS = "456789012345678", EstadoCivil = "Solteira", PossuiPlanoSaude = false },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Carlos Eduardo Mendes", CPF = "56789012345", DataNascimento = new DateTime(1982, 7, 25), Sexo = "Masculino", TipoSanguineo = "O-", Telefone = "(11)99999-5555", Email = "carlos@email.com", EnderecoCompleto = "Rua do Campo, 654", NumeroCartaoSUS = "567890123456789", EstadoCivil = "Casado", PossuiPlanoSaude = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Luciana Rodrigues", CPF = "67890123456", DataNascimento = new DateTime(1988, 11, 12), Sexo = "Feminino", TipoSanguineo = "A-", Telefone = "(11)99999-6666", Email = "luciana@email.com", EnderecoCompleto = "Av. Brasil, 987", NumeroCartaoSUS = "678901234567890", EstadoCivil = "União Estável", PossuiPlanoSaude = false },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Ricardo Almeida", CPF = "78901234567", DataNascimento = new DateTime(1975, 4, 8), Sexo = "Masculino", TipoSanguineo = "B+", Telefone = "(11)99999-7777", Email = "ricardo@email.com", EnderecoCompleto = "Rua Nova, 147", NumeroCartaoSUS = "789012345678901", EstadoCivil = "Viúvo", PossuiPlanoSaude = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Fernanda Castro", CPF = "89012345678", DataNascimento = new DateTime(1992, 9, 30), Sexo = "Feminino", TipoSanguineo = "AB-", Telefone = "(11)99999-8888", Email = "fernanda@email.com", EnderecoCompleto = "Rua Alegre, 258", NumeroCartaoSUS = "890123456789012", EstadoCivil = "Solteira", PossuiPlanoSaude = false },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "José Roberto Silva", CPF = "90123456789", DataNascimento = new DateTime(1980, 1, 14), Sexo = "Masculino", TipoSanguineo = "O+", Telefone = "(11)99999-9999", Email = "jose@email.com", EnderecoCompleto = "Av. Central, 369", NumeroCartaoSUS = "901234567890123", EstadoCivil = "Casado", PossuiPlanoSaude = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Sandra Pereira", CPF = "01234567890", DataNascimento = new DateTime(1987, 6, 27), Sexo = "Feminino", TipoSanguineo = "A+", Telefone = "(11)99999-0000", Email = "sandra@email.com", EnderecoCompleto = "Rua Esperança, 741", NumeroCartaoSUS = "012345678901234", EstadoCivil = "Divorciada", PossuiPlanoSaude = false }
                };

                context.Pacientes.AddRange(pacientes);
                context.SaveChanges();
            }

            // Seed Profissionais de Saúde
            if (!context.ProfissionaisSaude.Any())
            {
                var especialidades = context.Especialidades.ToList();
                var profissionais = new List<ProfissionalSaude>
                {
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Roberto Cardoso", CPF = "11111111111", Email = "roberto@hospital.com", Telefone = "(11)88888-1111", RegistroConselho = "CRM12345", TipoRegistro = "CRM", EspecialidadeId = especialidades[0].Id, DataAdmissao = DateTime.Now.AddYears(-5), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Mariana Pediatra", CPF = "22222222222", Email = "mariana@hospital.com", Telefone = "(11)88888-2222", RegistroConselho = "CRM23456", TipoRegistro = "CRM", EspecialidadeId = especialidades[1].Id, DataAdmissao = DateTime.Now.AddYears(-3), CargaHorariaSemanal = 36, Turno = "Tarde", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Paulo Ortopedista", CPF = "33333333333", Email = "paulo@hospital.com", Telefone = "(11)88888-3333", RegistroConselho = "CRM34567", TipoRegistro = "CRM", EspecialidadeId = especialidades[2].Id, DataAdmissao = DateTime.Now.AddYears(-7), CargaHorariaSemanal = 44, Turno = "Manhã", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Carla Neurologista", CPF = "44444444444", Email = "carla@hospital.com", Telefone = "(11)88888-4444", RegistroConselho = "CRM45678", TipoRegistro = "CRM", EspecialidadeId = especialidades[3].Id, DataAdmissao = DateTime.Now.AddYears(-4), CargaHorariaSemanal = 40, Turno = "Noite", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Anderson Ginecologista", CPF = "55555555555", Email = "anderson@hospital.com", Telefone = "(11)88888-5555", RegistroConselho = "CRM56789", TipoRegistro = "CRM", EspecialidadeId = especialidades[4].Id, DataAdmissao = DateTime.Now.AddYears(-6), CargaHorariaSemanal = 40, Turno = "Tarde", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Enfª. Julia Santos", CPF = "66666666666", Email = "julia@hospital.com", Telefone = "(11)88888-6666", RegistroConselho = "COREN67890", TipoRegistro = "COREN", EspecialidadeId = especialidades[9].Id, DataAdmissao = DateTime.Now.AddYears(-2), CargaHorariaSemanal = 36, Turno = "Manhã", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Beatriz Dermatologista", CPF = "77777777777", Email = "beatriz@hospital.com", Telefone = "(11)88888-7777", RegistroConselho = "CRM78901", TipoRegistro = "CRM", EspecialidadeId = especialidades[5].Id, DataAdmissao = DateTime.Now.AddYears(-3), CargaHorariaSemanal = 32, Turno = "Tarde", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Marcos Oftalmologista", CPF = "88888888888", Email = "marcos@hospital.com", Telefone = "(11)88888-8888", RegistroConselho = "CRM89012", TipoRegistro = "CRM", EspecialidadeId = especialidades[6].Id, DataAdmissao = DateTime.Now.AddYears(-8), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Patricia Psiquiatra", CPF = "99999999999", Email = "patricia@hospital.com", Telefone = "(11)88888-9999", RegistroConselho = "CRM90123", TipoRegistro = "CRM", EspecialidadeId = especialidades[7].Id, DataAdmissao = DateTime.Now.AddYears(-4), CargaHorariaSemanal = 40, Turno = "Tarde", Ativo = true },
                    new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Felipe Urologista", CPF = "00000000000", Email = "felipe@hospital.com", Telefone = "(11)88888-0000", RegistroConselho = "CRM01234", TipoRegistro = "CRM", EspecialidadeId = especialidades[8].Id, DataAdmissao = DateTime.Now.AddYears(-5), CargaHorariaSemanal = 44, Turno = "Manhã", Ativo = true }
                };

                context.ProfissionaisSaude.AddRange(profissionais);
                context.SaveChanges();
            }

            // Seed Prontuários
            if (!context.Prontuarios.Any())
            {
                var pacientes = context.Pacientes.ToList();
                var prontuarios = new List<Prontuario>();

                for (int i = 0; i < 10; i++)
                {
                    prontuarios.Add(new Prontuario
                    {
                        Id = Guid.NewGuid(),
                        Numero = $"PRONT{(i + 1):D6}",
                        DataAbertura = DateTime.Now.AddDays(-Random.Shared.Next(1, 365)),
                        ObservacoesGerais = $"Prontuário do paciente {pacientes[i].NomeCompleto}",
                        PacienteId = pacientes[i].Id
                    });
                }

                context.Prontuarios.AddRange(prontuarios);
                context.SaveChanges();
            }

            // Seed Atendimentos
            if (!context.Atendimentos.Any())
            {
                var pacientes = context.Pacientes.ToList();
                var profissionais = context.ProfissionaisSaude.ToList();
                var prontuarios = context.Prontuarios.ToList();
                var atendimentos = new List<Atendimento>();

                for (int i = 0; i < 10; i++)
                {
                    var tipos = new[] { "Emergência", "Consulta", "Internação" };
                    var status = new[] { "Realizado", "Em andamento", "Cancelado" };

                    atendimentos.Add(new Atendimento
                    {
                        Id = Guid.NewGuid(),
                        DataHora = DateTime.Now.AddDays(-Random.Shared.Next(0, 30)),
                        Tipo = tipos[Random.Shared.Next(tipos.Length)],
                        Status = status[Random.Shared.Next(status.Length)],
                        Local = $"Sala {i + 1:D2}",
                        PacienteId = pacientes[i].Id,
                        ProfissionalId = profissionais[i].Id,
                        ProntuarioId = prontuarios[i].Id
                    });
                }

                context.Atendimentos.AddRange(atendimentos);
                context.SaveChanges();
            }

            // Seed Prescrições
            if (!context.Prescricoes.Any())
            {
                var atendimentos = context.Atendimentos.ToList();
                var profissionais = context.ProfissionaisSaude.ToList();
                var prescricoes = new List<Prescricao>();

                var medicamentos = new[] { "Paracetamol", "Ibuprofeno", "Amoxicilina", "Dipirona", "Omeprazol", "Losartana", "Metformina", "Sinvastatina", "Captopril", "Atenolol" };
                var dosagens = new[] { "500mg", "200mg", "875mg", "500mg", "20mg", "50mg", "850mg", "20mg", "25mg", "50mg" };
                var frequencias = new[] { "8/8h", "12/12h", "8/8h", "6/6h", "24h", "24h", "12/12h", "24h", "8/8h", "12/12h" };
                var vias = new[] { "Oral", "Oral", "Oral", "Oral", "Oral", "Oral", "Oral", "Oral", "Oral", "Oral" };

                for (int i = 0; i < 10; i++)
                {
                    prescricoes.Add(new Prescricao
                    {
                        Id = Guid.NewGuid(),
                        AtendimentoId = atendimentos[i].Id,
                        ProfissionalId = profissionais[i].Id,
                        Medicamento = medicamentos[i],
                        Dosagem = dosagens[i],
                        Frequencia = frequencias[i],
                        ViaAdministracao = vias[i],
                        DataInicio = DateTime.Now.AddDays(-Random.Shared.Next(0, 15)),
                        DataFim = DateTime.Now.AddDays(Random.Shared.Next(5, 30)),
                        Observacoes = $"Prescrição para tratamento - {medicamentos[i]}",
                        StatusPrescricao = "Ativa",
                        ReacoesAdversas = "Nenhuma"
                    });
                }

                context.Prescricoes.AddRange(prescricoes);
                context.SaveChanges();
            }

            // Seed Exames
            if (!context.Exames.Any())
            {
                var atendimentos = context.Atendimentos.ToList();
                var exames = new List<Exame>();

                var tiposExame = new[] { "Hemograma", "Raio-X Tórax", "Ultrassom Abdominal", "Eletrocardiograma", "Tomografia", "Ressonância", "Urina Tipo I", "Glicemia", "Colesterol", "Creatinina" };
                var resultados = new[] { "Normal", "Alterado - consultar médico", "Dentro dos padrões", "Pequena alteração", "Normal", "Normal", "Sem alterações", "Normal", "Elevado", "Normal" };

                for (int i = 0; i < 10; i++)
                {
                    exames.Add(new Exame
                    {
                        Id = Guid.NewGuid(),
                        Tipo = tiposExame[i],
                        DataSolicitacao = DateTime.Now.AddDays(-Random.Shared.Next(1, 10)),
                        DataRealizacao = DateTime.Now.AddDays(-Random.Shared.Next(0, 5)),
                        Resultado = resultados[i],
                        AtendimentoId = atendimentos[i].Id
                    });
                }

                context.Exames.AddRange(exames);
                context.SaveChanges();
            }

            // Seed Internações
            if (!context.Internacoes.Any())
            {
                var pacientes = context.Pacientes.Take(5).ToList(); // Apenas 5 internações
                var atendimentos = context.Atendimentos.Take(5).ToList();
                var internacoes = new List<Internacao>();

                var motivos = new[] { "Pneumonia", "Fratura de fêmur", "Infarto agudo do miocárdio", "Apendicite", "Acidente vascular cerebral" };
                var setores = new[] { "UTI", "Ortopedia", "Cardiologia", "Cirurgia Geral", "Neurologia" };

                for (int i = 0; i < 5; i++)
                {
                    internacoes.Add(new Internacao
                    {
                        Id = Guid.NewGuid(),
                        PacienteId = pacientes[i].Id,
                        AtendimentoId = atendimentos[i].Id,
                        DataEntrada = DateTime.Now.AddDays(-Random.Shared.Next(1, 15)),
                        PrevisaoAlta = DateTime.Now.AddDays(Random.Shared.Next(3, 10)),
                        MotivoInternacao = motivos[i],
                        Leito = $"L{i + 1:D2}",
                        Quarto = $"Q{i + 101}",
                        Setor = setores[i],
                        PlanoSaudeUtilizado = pacientes[i].PossuiPlanoSaude ? "Unimed" : "",
                        ObservacoesClinicas = $"Paciente internado por {motivos[i].ToLower()}",
                        StatusInternacao = i < 3 ? "Ativa" : "Alta concedida"
                    });
                }

                context.Internacoes.AddRange(internacoes);
                context.SaveChanges();
            }

            // Seed Altas Hospitalares
            if (!context.AltasHospitalares.Any())
            {
                var internacoes = context.Internacoes.Where(i => i.StatusInternacao == "Alta concedida").ToList();
                var altas = new List<AltaHospitalar>();

                var condicoes = new[] { "Estável", "Melhorado", "Curado" };
                var instrucoes = new[] {
                    "Repouso domiciliar por 7 dias, retorno em 15 dias",
                    "Fisioterapia 3x por semana, medicação conforme prescrição",
                    "Dieta leve, evitar esforços físicos por 10 dias"
                };

                for (int i = 0; i < internacoes.Count; i++)
                {
                    altas.Add(new AltaHospitalar
                    {
                        Id = Guid.NewGuid(),
                        DataAlta = DateTime.Now.AddDays(-Random.Shared.Next(0, 5)),
                        CondicaoPaciente = condicoes[Random.Shared.Next(condicoes.Length)],
                        InstrucoesPosAlta = instrucoes[i % instrucoes.Length],
                        InternacaoId = internacoes[i].Id
                    });
                }

                context.AltasHospitalares.AddRange(altas);
                context.SaveChanges();
            }
        }
    }
}

