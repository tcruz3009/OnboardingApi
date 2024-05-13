using Microsoft.Extensions.Caching.Memory;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using OnboardingApi.Infrastructure;

namespace OnboardingApi.Services
{
  public class TotverService(
    ITotverRepository repository,
    IUnitOfWork unitOfWork,
    IMemoryCache cache,
    ILogger<TotverService> logger
    ) : ITotverService
  {
    public async Task<IEnumerable<Totver>> ListAsync()
    {
      var result = await cache.GetOrCreateAsync(CacheKeys.TotversList, (entry) =>
      {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        return repository.ListAsync();
      });

      return result ?? [];
    }

    public async Task<Response<Totver>> SaveAsync(Totver totver)
    {
      try
      {
        await repository.AddAsync(totver);
        await unitOfWork.CompleteAsync();

        return new Response<Totver>(totver);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel salvar o registro. Erro: {ex.Message}");
        return new Response<Totver>(ErrorType.Error, "Um erro ocorreu tentando salvar o registro.", ex.Message);
      }
    }

    public async Task<Response<Totver>> UpdateAsync(Guid id, Totver totver)
    {
      var existingTotver = await repository.GetByIdAsync(id);
      if (existingTotver == null)
      {
        return new Response<Totver>(ErrorType.Error, "Registro não encontrado.");
      }

      existingTotver = totver;

      try
      {
        await unitOfWork.CompleteAsync();
        return new Response<Totver>(existingTotver);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel atualizar o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Totver>(ErrorType.Error, "Um erro ocorreu tentando atualizar o registro.", ex.Message);
      }
    }

    public async Task<Response<Totver>> DeleteAsync(Guid id)
    {
      var existingTotver = await repository.GetByIdAsync(id);
      if (existingTotver == null)
      {
        return new Response<Totver>(ErrorType.Error, "Registro não encontrado.");
      }

      try
      {
        repository.Remove(existingTotver);
        await unitOfWork.CompleteAsync();

        return new Response<Totver>(existingTotver);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel  excluir o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Totver>(ErrorType.Error, "Um erro ocorreu tentando excluir o registro.", ex.Message);
      }
    }
  }
}
