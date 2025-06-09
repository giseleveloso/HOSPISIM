using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class AltaHospitalar
    {
        public Guid Id { get; set; }

        public DateTime DataAlta { get; set; }

        [StringLength(100)]
        public string CondicaoPaciente { get; set; }

        [StringLength(1000)]
        public string InstrucoesPosAlta { get; set; }

        public Guid InternacaoId { get; set; }

        // Navigation Properties
        public virtual Internacao Internacao { get; set; }
    }
}