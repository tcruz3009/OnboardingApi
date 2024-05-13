using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnboardingApi.Domain.Enums;

namespace OnboardingApi.Domain.Dtos
{
  public class AtividadeOnboardingDto
  {
    public int? Id { get; set; }
    public Guid OnboardingId { get; set; }
    public Guid AtividadeId { get; set; }
    public StatusEnum StatusAtividade { get; set; }
    public string? Comentário { get; set; }
  }
}
