using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using System.Net;

namespace OnboardingApi.Controllers
{
	public class AtividadesOnboardingsController : BaseApiController
	{
		private readonly IAtividadeOnboardingService _AtividadeOnboardingService;
		private readonly IMapper _mapper;

		public AtividadesOnboardingsController(IAtividadeOnboardingService AtividadeOnboardingService, IMapper mapper)
		{
			_AtividadeOnboardingService = AtividadeOnboardingService;
			_mapper = mapper;
		}

		/// <summary>
		/// Lists all categories.
		/// </summary>
		/// <returns>List os categories.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AtividadeOnboardingDto>), 200)]
		public async Task<IEnumerable<AtividadeOnboardingDto>> ListAsync()
		{
			var result = await _AtividadeOnboardingService.ListAsync();
			return _mapper.Map<IEnumerable<AtividadeOnboardingDto>>(result);
		}

		/// <summary>
		/// Saves a new AtividadeOnboarding.
		/// </summary>
		/// <param name="resource">AtividadeOnboarding data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(AtividadeOnboardingDto), 201)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PostAsync([FromBody] AtividadeOnboardingDto resource)
		{
			var entity = _mapper.Map<AtividadeOnboarding>(resource);
			var result = await _AtividadeOnboardingService.SaveAsync(entity);

			if (result._message != null)
			{
				result._message.code = HttpStatusCode.BadRequest.ToString();
				return BadRequest(result._message);
			}

			var data = _mapper.Map<AtividadeOnboardingDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Updates an existing AtividadeOnboarding according to an identifier.
		/// </summary>
		/// <param name="id">AtividadeOnboarding identifier.</param>
		/// <param name="resource">Updated AtividadeOnboarding data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(AtividadeOnboardingDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PutAsync(int id, [FromBody] AtividadeOnboardingDto resource)
		{
			var AtividadeOnboarding = _mapper.Map<AtividadeOnboarding>(resource);
			var result = await _AtividadeOnboardingService.UpdateAsync(id, AtividadeOnboarding);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<AtividadeOnboardingDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Deletes a given AtividadeOnboarding according to an identifier.
		/// </summary>
		/// <param name="id">AtividadeOnboarding identifier.</param>
		/// <returns>Response for the request.</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(AtividadeOnboardingDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var result = await _AtividadeOnboardingService.DeleteAsync(id);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<AtividadeOnboardingDto>(result.Data!);
			return Ok(data);
		}
	}
}