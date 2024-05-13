using System.ComponentModel.DataAnnotations;

namespace OnboardingApi.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public required string CriadoPor { get; set; }
        public DateTime? UltimaAlteracao { get; set; }
        public string? AlteradoPor { get; set; }
    }
}
