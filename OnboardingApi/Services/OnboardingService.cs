using Microsoft.Extensions.Caching.Memory;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using OnboardingApi.Infrastructure;

namespace OnboardingApi.Services
{
  public class OnboardingService : IOnboardingService
  {
    private readonly IOnboardingRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly ILogger<OnboardingService> _logger;

    public OnboardingService
    (
      IOnboardingRepository repository,
      IUnitOfWork unitOfWork,
      IMemoryCache cache,
      ILogger<OnboardingService> logger
    )
    {
      _repository = repository;
      _unitOfWork = unitOfWork;
      _cache = cache;
      _logger = logger;
    }

    public async Task<IEnumerable<Onboarding>> ListAsync()
    {
      var result = await _cache.GetOrCreateAsync(CacheKeys.OnboardingsList, (entry) =>
      {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        return _repository.ListAsync();
      });

      return result ?? new List<Onboarding>();
    }

    public async Task<Response<Onboarding>> SaveAsync(Onboarding onboarding)
    {
      try
      {
        await _repository.AddAsync(onboarding);
        await _unitOfWork.CompleteAsync();

        return new Response<Onboarding>(onboarding);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel salvar o registro. Erro: {ex.Message}");
        return new Response<Onboarding>(ErrorType.Error, "Um erro ocorreu tentando salvar o registro.", ex.Message);
      }
    }

    public async Task<Response<Onboarding>> UpdateAsync(int id, Onboarding onboarding)
    {
      var existingOnboarding = await _repository.GetByIdAsync(id);
      if (existingOnboarding == null)
      {
        return new Response<Onboarding>(ErrorType.Error, "Registro não encontrado.");
      }

      existingOnboarding = onboarding;

      try
      {
        await _unitOfWork.CompleteAsync();
        return new Response<Onboarding>(existingOnboarding);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel atualizar o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Onboarding>(ErrorType.Error, "Um erro ocorreu tentando atualizar o registro.", ex.Message);
      }
    }

    public async Task<Response<Onboarding>> DeleteAsync(int id)
    {
      var existingOnboarding = await _repository.GetByIdAsync(id);
      if (existingOnboarding == null)
      {
        return new Response<Onboarding>(ErrorType.Error, "Registro não encontrado.");
      }

      try
      {
        _repository.Remove(existingOnboarding);
        await _unitOfWork.CompleteAsync();

        return new Response<Onboarding>(existingOnboarding);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel  excluir o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Onboarding>(ErrorType.Error, "Um erro ocorreu tentando excluir o registro.", ex.Message);
      }
    }
  }
}
