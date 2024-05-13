using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnboardingApi.Domain.Enums;
using OnboardingApi.Dtos;
using System.ComponentModel.DataAnnotations;

namespace OnboardingApi.Domain.Dtos
{
  public class AtividadeOnboardingDto : BaseDto
  {
    [Required(ErrorMessage = "O identificador do onboarding é Obrigatório")]
    public Guid OnboardingId { get; set; }

    [Required(ErrorMessage = "O identificador da atividade é Obrigatório")]
    public Guid AtividadeId { get; set; }
    public StatusEnum StatusAtividade { get; set; } = StatusEnum.NaoIniciado;
    public string? Comentário { get; set; }
  }
}
