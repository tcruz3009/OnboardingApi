using Microsoft.EntityFrameworkCore;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Persistence.Context;

namespace OnboardingApi.Persistence.Repositories
{
  public class AtividadeRepository(AppDbContext context) : BaseRepository(context), IAtividadeRepository
  {
    public async Task<IEnumerable<Atividade>> ListAsync() 
      => await _context.Atividades.AsNoTracking().ToListAsync();

    public async Task<Atividade?> GetByIdAsync(Guid id)
      => await _context.Atividades.FindAsync(id);

    public async Task AddAsync(Atividade entity) 
      => await _context.Atividades.AddAsync(entity);

    public void Update(Atividade entity)
    {
      _context.Atividades.Update(entity);
    }

    public void Remove(Atividade entity)
    {
      _context.Atividades.Remove(entity);
    }


  }
}