using OnboardingApi.Dtos;
using System.ComponentModel.DataAnnotations;

namespace OnboardingApi.Domain.Dtos
{
  public class AtividadeDto : BaseDto
  {
    public  bool Ativo { get; set; } = true;

    [Required(ErrorMessage = "A descrição da atividade é Obrigatório")]
    [MinLength(3, ErrorMessage = "Tamanho mínimo da descrição são 3 caracteres")]
    [MaxLength(255, ErrorMessage = "Tamanho máximo da descrição são 255 caracteres")]
    public required string Descricao { get; set; }
    [Required]
    public bool Obrigatório { get; set; } = false;

    [Required(ErrorMessage = "O como fazer da atividade é Obrigatório")]
    [MinLength(3, ErrorMessage = "Tamanho mínimo do texto exigido 3 caracteres")]
    [MaxLength(2000, ErrorMessage = "Tamanho máximo do e-mail são 2000 caracteres")]
    public string? ComoFazer { get; set; }

    [Required(ErrorMessage = "O tempo esperado da atividade é Obrigatório")]
    public int? TempoEstimado { get; set; }

    [Required(ErrorMessage = "A classificação da atividade é Obrigatório")]
    [MinLength(3, ErrorMessage = "Tamanho mínimo do texto exigido 3 caracteres")]
    [MaxLength(255, ErrorMessage = "Tamanho máximo do e-mail são 255 caracteres")]
    public string? Classificacao { get; set; }

  }
}
