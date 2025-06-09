using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class ProfissionalSaude
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string NomeCompleto { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [StringLength(20)]
        public string RegistroConselho { get; set; }

        [StringLength(10)]
        public string TipoRegistro { get; set; }

        public Guid EspecialidadeId { get; set; }

        public DateTime DataAdmissao { get; set; }

        public int CargaHorariaSemanal { get; set; }

        [StringLength(20)]
        public string Turno { get; set; }

        public bool Ativo { get; set; }

        // Navigation Properties
        public virtual Especialidade Especialidade { get; set; }
        public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
        public virtual ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
    }
}
