using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class Exame
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Tipo { get; set; } // Sangue, Imagem, Urina, etc.

        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataRealizacao { get; set; }

        [StringLength(2000)]
        public string Resultado { get; set; }

        public Guid AtendimentoId { get; set; }

        // Navigation Properties
        public virtual Atendimento Atendimento { get; set; }
    }
}
