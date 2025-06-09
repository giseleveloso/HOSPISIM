using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class Especialidade
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        // Navigation Properties
        public virtual ICollection<ProfissionalSaude> Profissionais { get; set; } = new List<ProfissionalSaude>();
    }
}
