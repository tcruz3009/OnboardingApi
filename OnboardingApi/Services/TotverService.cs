using Microsoft.Extensions.Caching.Memory;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using OnboardingApi.Infrastructure;

namespace OnboardingApi.Services
{
  public class TotverService : ITotverService
  {
    private readonly ITotverRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly ILogger<TotverService> _logger;

    public TotverService
    (
      ITotverRepository repository,
      IUnitOfWork unitOfWork,
      IMemoryCache cache,
      ILogger<TotverService> logger
    )
    {
      _repository = repository;
      _unitOfWork = unitOfWork;
      _cache = cache;
      _logger = logger;
    }

    public async Task<IEnumerable<Totver>> ListAsync()
    {
      var result = await _cache.GetOrCreateAsync(CacheKeys.TotversList, (entry) =>
      {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        return _repository.ListAsync();
      });

      return result ?? new List<Totver>();
    }

    public async Task<Response<Totver>> SaveAsync(Totver Totver)
    {
      try
      {
        await _repository.AddAsync(Totver);
        await _unitOfWork.CompleteAsync();

        return new Response<Totver>(Totver);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel salvar o registro. Erro: {ex.Message}");
        return new Response<Totver>(ErrorType.Error, "Um erro ocorreu tentando salvar o registro.", ex.Message);
      }
    }

    public async Task<Response<Totver>> UpdateAsync(int id, Totver Totver)
    {
      var existingTotver = await _repository.GetByIdAsync(id);
      if (existingTotver == null)
      {
        return new Response<Totver>(ErrorType.Error, "Registro não encontrado.");
      }

      existingTotver = Totver;

      try
      {
        await _unitOfWork.CompleteAsync();
        return new Response<Totver>(existingTotver);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel atualizar o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Totver>(ErrorType.Error, "Um erro ocorreu tentando atualizar o registro.", ex.Message);
      }
    }

    public async Task<Response<Totver>> DeleteAsync(int id)
    {
      var existingTotver = await _repository.GetByIdAsync(id);
      if (existingTotver == null)
      {
        return new Response<Totver>(ErrorType.Error, "Registro não encontrado.");
      }

      try
      {
        _repository.Remove(existingTotver);
        await _unitOfWork.CompleteAsync();

        return new Response<Totver>(existingTotver);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel  excluir o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Totver>(ErrorType.Error, "Um erro ocorreu tentando excluir o registro.", ex.Message);
      }
    }
  }
}
