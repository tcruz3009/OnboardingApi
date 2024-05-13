using Microsoft.Extensions.Caching.Memory;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Repositories;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using OnboardingApi.Infrastructure;

namespace OnboardingApi.Services
{
  public class AtividadeService : IAtividadeService
  {
    private readonly IAtividadeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AtividadeService> _logger;

    public AtividadeService
    (
      IAtividadeRepository repository,
      IUnitOfWork unitOfWork,
      IMemoryCache cache,
      ILogger<AtividadeService> logger
    )
    {
      _repository = repository;
      _unitOfWork = unitOfWork;
      _cache = cache;
      _logger = logger;
    }

    public async Task<IEnumerable<Atividade>> ListAsync()
    {
      var result = await _cache.GetOrCreateAsync(CacheKeys.AtividadesList, (entry) =>
      {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
        return _repository.ListAsync();
      });

      return result ?? new List<Atividade>();
    }

    public async Task<Response<Atividade>> SaveAsync(Atividade atividade)
    {
      try
      {
        await _repository.AddAsync(atividade);
        await _unitOfWork.CompleteAsync();

        return new Response<Atividade>(atividade);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel salvar o registro. Erro: {ex.Message}");
        return new Response<Atividade>(ErrorType.Error, "Um erro ocorreu tentando salvar o registro.", ex.Message);
      }
    }

    public async Task<Response<Atividade>> UpdateAsync(int id, Atividade atividade)
    {
      var existingAtividade = await _repository.GetByIdAsync(id);
      if (existingAtividade == null)
      {
        return new Response<Atividade>(ErrorType.Error, "Registro não encontrado.");
      }

      existingAtividade = atividade;

      try
      {
        await _unitOfWork.CompleteAsync();
        return new Response<Atividade>(existingAtividade);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel atualizar o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Atividade>(ErrorType.Error, "Um erro ocorreu tentando atualizar o registro.", ex.Message);
      }
    }

    public async Task<Response<Atividade>> DeleteAsync(int id)
    {
      var existingAtividade = await _repository.GetByIdAsync(id);
      if (existingAtividade == null)
      {
        return new Response<Atividade>(ErrorType.Error, "Registro não encontrado.");
      }

      try
      {
        _repository.Remove(existingAtividade);
        await _unitOfWork.CompleteAsync();

        return new Response<Atividade>(existingAtividade);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Não foi possivel  excluir o registro com o ID {id}. Erro: {ex.Message}");
        return new Response<Atividade>(ErrorType.Error, "Um erro ocorreu tentando excluir o registro.", ex.Message);
      }
    }
  }
}
