using Microsoft.Extensions.Caching.Memory;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using OnboardingApi.Infrastructure;

namespace OnboardingApi.Services
{
  public class AtividadeOnboardingService : IAtividadeOnboardingService
  {
    private readonly IAtividadeOnboardingRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AtividadeOnboardingService> _logger;

    public AtividadeOnboardingService
    (
      IAtividadeOnboardingRepository repository,
      IUnitOfWork unitOfWork,
      IMemoryCache cache,
      ILogger<AtividadeOnboardingService> logger
    )
    {
      _repository = repository;
      _unitOfWork = unitOfWork;
      _cache = cache;
      _logger = logger;
    }

    public async Task<IEnumerable<AtividadeOnboarding>> ListAsync()
    {
      var result = await _cache.GetOrCreateAsync(CacheKeys.AtividadesOnboardingsList, (entry) =>
      {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        return _repository.ListAsync();
      });

      return result ?? new List<AtividadeOnboarding>();
    }

    public async Task<Response<AtividadeOnboarding>> SaveAsync(AtividadeOnboarding atividadeOnboarding)
    {
      try
      {
        await _repository.AddAsync(atividadeOnboarding);
        await _unitOfWork.CompleteAsync();

        return new Response<AtividadeOnboarding>(atividadeOnboarding);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel salvar o registro. Erro: {ex.Message}");
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Um erro ocorreu tentando salvar o registro.", ex.Message);
      }
    }

    public async Task<Response<AtividadeOnboarding>> UpdateAsync(int id, AtividadeOnboarding atvOnboarding)
    {
      var existingAtvOnboarding = await _repository.GetByIdAsync(id);
      if (existingAtvOnboarding == null)
      {
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Registro não encontrado.");
      }

      existingAtvOnboarding = atvOnboarding;

      try
      {
        await _unitOfWork.CompleteAsync();
        return new Response<AtividadeOnboarding>(existingAtvOnboarding);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel atualizar o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Um erro ocorreu tentando atualizar o registro.", ex.Message);
      }
    }

    public async Task<Response<AtividadeOnboarding>> DeleteAsync(int id)
    {
      var existingAtvOnboarding = await _repository.GetByIdAsync(id);
      if (existingAtvOnboarding == null)
      {
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Registro não encontrado.");
      }

      try
      {
        _repository.Remove(existingAtvOnboarding);
        await _unitOfWork.CompleteAsync();

        return new Response<AtividadeOnboarding>(existingAtvOnboarding);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel  excluir o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<AtividadeOnboarding>(ErrorType.Error, "Um erro ocorreu tentando excluir o registro.", ex.Message);
      }
    }
  }
}
