using Microsoft.Extensions.Caching.Memory;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using OnboardingApi.Infrastructure;

namespace OnboardingApi.Services
{
  public class AtividadeService(
    IAtividadeRepository repository,
    IUnitOfWork unitOfWork,
    IMemoryCache cache,
    ILogger<AtividadeService> logger
    ) : IAtividadeService
  {
    public async Task<IEnumerable<Atividade>> ListAsync()
    {
      var result = await cache.GetOrCreateAsync(CacheKeys.AtividadesList, (entry) =>
      {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
        return repository.ListAsync();
      });

      return result ?? [];
    }

    public async Task<Response<Atividade>> SaveAsync(Atividade atividade)
    {
      try
      {
        await repository.AddAsync(atividade);
        await unitOfWork.CompleteAsync();

        return new Response<Atividade>(atividade);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel salvar o registro. Erro: {ex.Message}");
        return new Response<Atividade>(ErrorType.Error, "Um erro ocorreu tentando salvar o registro.", ex.Message);
      }
    }

    public async Task<Response<Atividade>> UpdateAsync(Guid id, Atividade atividade)
    {
      var existingAtividade = await repository.GetByIdAsync(id);
      if (existingAtividade == null)
      {
        return new Response<Atividade>(ErrorType.Error, "Registro não encontrado.");
      }

      existingAtividade = atividade;

      try
      {
        await unitOfWork.CompleteAsync();
        return new Response<Atividade>(existingAtividade);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel atualizar o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Atividade>(ErrorType.Error, "Um erro ocorreu tentando atualizar o registro.", ex.Message);
      }
    }

    public async Task<Response<Atividade>> DeleteAsync(Guid id)
    {
      var existingAtividade = await repository.GetByIdAsync(id);
      if (existingAtividade == null)
      {
        return new Response<Atividade>(ErrorType.Error, "Registro não encontrado.");
      }

      try
      {
        repository.Remove(existingAtividade);
        await unitOfWork.CompleteAsync();

        return new Response<Atividade>(existingAtividade);
      }
      catch (Exception ex)
      {
        logger.LogError("{message}", $"Não foi possivel  excluir o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Atividade>(ErrorType.Error, "Um erro ocorreu tentando excluir o registro.", ex.Message);
      }
    }
  }
}
