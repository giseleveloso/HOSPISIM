using HOSPISIM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HOSPISIM.Persistence
{
    public class HospiSimDbContext : DbContext
    {
        public HospiSimDbContext(DbContextOptions<HospiSimDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<ProfissionalSaude> ProfissionaisSaude { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Internacao> Internacoes { get; set; }
        public DbSet<AltaHospitalar> AltasHospitalares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de relacionamentos

            // Paciente -> Prontuario (1:N)
            modelBuilder.Entity<Prontuario>()
                .HasOne(p => p.Paciente)
                .WithMany(pac => pac.Prontuarios)
                .HasForeignKey(p => p.PacienteId);

            // Prontuario -> Atendimento (1:N)
            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Prontuario)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(a => a.ProntuarioId);

            // Profissional -> Atendimento (1:N)
            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Profissional)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(a => a.ProfissionalId);

            // Paciente -> Atendimento (1:N) - adicional
            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Paciente)
                .WithMany()
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);

            // Atendimento -> Prescricao (1:N)
            modelBuilder.Entity<Prescricao>()
                .HasOne(p => p.Atendimento)
                .WithMany(a => a.Prescricoes)
                .HasForeignKey(p => p.AtendimentoId);

            // Profissional -> Prescricao (1:N)
            modelBuilder.Entity<Prescricao>()
                .HasOne(p => p.Profissional)
                .WithMany(prof => prof.Prescricoes)
                .HasForeignKey(p => p.ProfissionalId)
                .OnDelete(DeleteBehavior.NoAction);

            // Atendimento -> Exame (1:N)
            modelBuilder.Entity<Exame>()
                .HasOne(e => e.Atendimento)
                .WithMany(a => a.Exames)
                .HasForeignKey(e => e.AtendimentoId);

            // Atendimento -> Internacao (0..1:1)
            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.Atendimento)
                .WithOne(a => a.Internacao)
                .HasForeignKey<Internacao>(i => i.AtendimentoId);

            // Paciente -> Internacao (1:N)
            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Internacoes)
                .HasForeignKey(i => i.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);

            // Internacao -> AltaHospitalar (0..1:1)
            modelBuilder.Entity<AltaHospitalar>()
                .HasOne(a => a.Internacao)
                .WithOne(i => i.AltaHospitalar)
                .HasForeignKey<AltaHospitalar>(a => a.InternacaoId);

            // Especialidade -> Profissional (1:N)
            modelBuilder.Entity<ProfissionalSaude>()
                .HasOne(p => p.Especialidade)
                .WithMany(e => e.Profissionais)
                .HasForeignKey(p => p.EspecialidadeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
