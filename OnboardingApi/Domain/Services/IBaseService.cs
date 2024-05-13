using OnboardingApi.Domain.Services.Communication;

namespace OnboardingApi.Domain.Services
{
  public interface IBaseService<T> where T : class
  {
    Task<IEnumerable<T>> ListAsync();
    Task<Response<T>> SaveAsync(T category);
    Task<Response<T>> UpdateAsync(Guid id, T category);
    Task<Response<T>> DeleteAsync(Guid id);
  }
}
