using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class Prontuario
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        public DateTime DataAbertura { get; set; }

        [StringLength(1000)]
        public string ObservacoesGerais { get; set; }

        public Guid PacienteId { get; set; }

        // Navigation Properties
        public virtual Paciente Paciente { get; set; }
        public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
    }
}