using Microsoft.Extensions.Caching.Memory;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using OnboardingApi.Infrastructure;

namespace OnboardingApi.Services
{
  public class AtividadeOnboardingService(
    IAtividadeOnboardingRepository repository,
    IUnitOfWork unitOfWork,
    IMemoryCache cache,
    ILogger<AtividadeOnboardingService> logger
    ) : IAtividadeOnboardingService
  {
    public async Task<IEnumerable<AtividadeOnboarding>> ListAsync()
    {
      var result = await cache.GetOrCreateAsync(CacheKeys.AtividadesOnboardingsList, (entry) =>
      {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        return repository.ListAsync();
      });

      return result ?? [];
    }

    public async Task<Response<AtividadeOnboarding>> SaveAsync(AtividadeOnboarding atividadeOnboarding)
    {
      try
      {
        await repository.AddAsync(atividadeOnboarding);
        await unitOfWork.CompleteAsync();

        return new Response<AtividadeOnboarding>(atividadeOnboarding);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel salvar o registro. Erro: {ex.Message}");
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Um erro ocorreu tentando salvar o registro.", ex.Message);
      }
    }

    public async Task<Response<AtividadeOnboarding>> UpdateAsync(Guid id, AtividadeOnboarding atvOnboarding)
    {
      var existingAtvOnboarding = await repository.GetByIdAsync(id);
      if (existingAtvOnboarding == null)
      {
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Registro não encontrado.");
      }

      existingAtvOnboarding = atvOnboarding;

      try
      {
        await unitOfWork.CompleteAsync();
        return new Response<AtividadeOnboarding>(existingAtvOnboarding);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel atualizar o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Um erro ocorreu tentando atualizar o registro.", ex.Message);
      }
    }

    public async Task<Response<AtividadeOnboarding>> DeleteAsync(Guid id)
    {
      var existingAtvOnboarding = await repository.GetByIdAsync(id);
      if (existingAtvOnboarding == null)
      {
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Registro não encontrado.");
      }

      try
      {
        repository.Remove(existingAtvOnboarding);
        await unitOfWork.CompleteAsync();

        return new Response<AtividadeOnboarding>(existingAtvOnboarding);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel  excluir o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Um erro ocorreu tentando excluir o registro.", ex.Message);
      }
    }
  }
}
