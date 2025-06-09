using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class Prescricao
    {
        public Guid Id { get; set; }

        public Guid AtendimentoId { get; set; }
        public Guid ProfissionalId { get; set; }

        [Required]
        [StringLength(200)]
        public string Medicamento { get; set; }

        [StringLength(50)]
        public string Dosagem { get; set; }

        [StringLength(100)]
        public string Frequencia { get; set; }

        [StringLength(50)]
        public string ViaAdministracao { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        [StringLength(500)]
        public string Observacoes { get; set; }

        [StringLength(50)]
        public string StatusPrescricao { get; set; } // Ativa, Suspensa, Encerrada

        [StringLength(500)]
        public string ReacoesAdversas { get; set; }

        // Navigation Properties
        public virtual Atendimento Atendimento { get; set; }
        public virtual ProfissionalSaude Profissional { get; set; }
    }
}