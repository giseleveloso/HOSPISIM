using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HOSPISIM.Models
{
    public class Atendimento
    {
        public Guid Id { get; set; }

        public DateTime DataHora { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; } // Emergência, Consulta, Internação

        [StringLength(50)]
        public string Status { get; set; } // Realizado, Em andamento, Cancelado

        [StringLength(100)]
        public string Local { get; set; }

        public Guid PacienteId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid ProntuarioId { get; set; }

        // Navigation Properties
        public virtual Paciente Paciente { get; set; }
        public virtual ProfissionalSaude Profissional { get; set; }
        public virtual Prontuario Prontuario { get; set; }
        public virtual ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
        public virtual ICollection<Exame> Exames { get; set; } = new List<Exame>();
        public virtual Internacao Internacao { get; set; }
    }
}