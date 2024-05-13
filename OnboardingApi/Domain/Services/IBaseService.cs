using OnboardingApi.Domain.Services.Communication;

namespace OnboardingApi.Domain.Services
{
  public interface IBaseService<T> where T : class
  {
    Task<IEnumerable<T>> ListAsync();
    Task<Response<T>> SaveAsync(T entity);
    Task<Response<T>> UpdateAsync(Guid id, T entity);
    Task<Response<T>> DeleteAsync(Guid id);
  }
}
