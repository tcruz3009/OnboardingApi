using OnboardingApi.Domain.Repositories;
using OnboardingApi.Persistence.Context;

namespace OnboardingApi.Persistence.Repositories
{
  public class UnitOfWork(AppDbContext context) : IUnitOfWork
  {
    private readonly AppDbContext _context = context;

    public async Task CompleteAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}