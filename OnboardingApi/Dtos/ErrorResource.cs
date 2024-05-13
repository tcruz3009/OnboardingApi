using OnboardingApi.Domain.Services.Communication;

namespace OnboardingApi.Domain.Dtos
{
  public record ErrorResource
  {
    public List<ErrorMessage> _messages { get; private set; }

    public ErrorResource(List<ErrorMessage> messages)
    {
      _messages = messages ?? [];
    }

    public ErrorResource(ErrorMessage message)
    {
      _messages = [];

      this._messages.Add(message);

    }
  }
}