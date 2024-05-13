using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnboardingApi.Domain.Enums;

namespace OnboardingApi.Domain.Models
{
  [PrimaryKey(nameof(OnboardingId), nameof(AtividadeId))]
  public class AtividadeOnboarding
  {

    public Guid OnboardingId { get; set; }
    public Onboarding Onboarding { get; set; } = null!;
    public Guid AtividadeId { get; set; }
    public Atividade Atividade { get; set; } = null!;
    public StatusEnum StatusAtividade { get; set; }
    public string? Comentário { get; set; }
    public DateTime DataCriacao { get; set; }
    public required string CriadoPor { get; set; }
    public DateTime? UltimaAlteracao { get; set; }
    public string? AlteradoPor { get; set; }
  }
}
