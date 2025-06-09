using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class Internacao
    {
        public Guid Id { get; set; }

        public Guid PacienteId { get; set; }
        public Guid AtendimentoId { get; set; }

        public DateTime DataEntrada { get; set; }
        public DateTime? PrevisaoAlta { get; set; }

        [StringLength(500)]
        public string MotivoInternacao { get; set; }

        [StringLength(20)]
        public string Leito { get; set; }

        [StringLength(20)]
        public string Quarto { get; set; }

        [StringLength(50)]
        public string Setor { get; set; } // UTI, Clínica Geral, Pediatria

        [StringLength(100)]
        public string PlanoSaudeUtilizado { get; set; }

        [StringLength(1000)]
        public string ObservacoesClinicas { get; set; }

        [StringLength(50)]
        public string StatusInternacao { get; set; } // Ativa, Alta concedida, Transferido, Óbito

        // Navigation Properties
        public virtual Paciente Paciente { get; set; }
        public virtual Atendimento Atendimento { get; set; }
        public virtual AltaHospitalar AltaHospitalar { get; set; }
    }
}
