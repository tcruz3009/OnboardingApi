namespace OnboardingApi.Dtos
{
  public class BaseDto
  {
    public Guid? Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public required string CriadoPor { get; set; }
    public DateTime? UltimaAlteracao { get; set; }
    public string? AlteradoPor { get; set; }
  }
}
