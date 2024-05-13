using System.ComponentModel.DataAnnotations;
using OnboardingApi.Domain.Enums;

namespace OnboardingApi.Domain.Models
{
    public class Onboarding : BaseEntity
    {
        [Required]
        public Guid PadrinhoId { get; set; }
        [Required]
        public required Totver Padrinho { get; set; }
        [Required]
        public Guid NovoTotverId { get; set; }
        [Required]
        public required Totver NovoTotver { get; set; }

        public StatusEnum StatusOnboarding { get; set; }

        public virtual required ICollection<AtividadeOnboarding> Atividades { get; set; }
    }
}
