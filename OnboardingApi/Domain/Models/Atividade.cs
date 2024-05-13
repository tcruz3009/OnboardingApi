using System.ComponentModel.DataAnnotations;

namespace OnboardingApi.Domain.Models
{
    public class Atividade : BaseEntity
    {
        [Required]
        public required bool Ativo { get; set; }
        [Required]
        public required string Descricao { get; set; }
        [Required]
        public required bool Obrigatório { get; set; }
        public string? ComoFazer { get; set; }
        public int TempoEstimado { get; set; }
        [Required]
        public required string Classificacao { get; set; }

        public virtual required ICollection<AtividadeOnboarding> Oboardings { get; set; }
    }
}
