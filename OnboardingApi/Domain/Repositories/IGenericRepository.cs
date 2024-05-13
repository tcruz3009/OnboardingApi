namespace OnboardingApi.Domain.Repositories
{
  public interface IGenericRepository<T> where T : class
  {
    Task<IEnumerable<T>> ListAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
  }
}
