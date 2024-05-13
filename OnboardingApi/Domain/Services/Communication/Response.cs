using Azure;
using System.Net;

namespace OnboardingApi.Domain.Services.Communication
{
  public record Response<T>
  {
    public ErrorMessage? _message { get; init; }
    public T? Data { get; init; }

    public Response(T resource)
    {
      _message = null;
      Data = resource;
    }

    public Response(ErrorType type, string message = "", string detailedMessage = "")
    {
      _message = new ErrorMessage()
      {
        type = type.Value,
        message = message,
        detailedMessage = detailedMessage
      };
      Data = default;
    }
  }

  public class ErrorMessage
  {
    public string? code { get; set; }
    public string? type { get; set; }
    public string? message { get; set; }
    public string? detailedMessage { get; set; }
    public string? helpUrl { get; set; }
    public List<Response>? details { get; set; }
  }

  public class ErrorType
  {
    private ErrorType(string value) { Value = value; }

    public string Value { get; private set; }

    public static ErrorType Info { get { return new ErrorType("Info"); } }
    public static ErrorType Warning { get { return new ErrorType("Warning"); } }
    public static ErrorType Error { get { return new ErrorType("Error"); } }
  }
}