using System.ComponentModel.DataAnnotations;

namespace HOSPISIM.Models
{
    public class Paciente
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string NomeCompleto { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        [StringLength(20)]
        public string Sexo { get; set; }

        [StringLength(5)]
        public string TipoSanguineo { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(300)]
        public string EnderecoCompleto { get; set; }

        [StringLength(20)]
        public string NumeroCartaoSUS { get; set; }

        [StringLength(20)]
        public string EstadoCivil { get; set; }

        public bool PossuiPlanoSaude { get; set; }

        // Navigation Properties
        public virtual ICollection<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public virtual ICollection<Internacao> Internacoes { get; set; } = new List<Internacao>();
    }
}

