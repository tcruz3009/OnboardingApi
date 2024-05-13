using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using System.Net;

namespace OnboardingApi.Controllers
{
	public class AtividadesOnboardingsController(IAtividadeOnboardingService AtividadeOnboardingService, IMapper mapper) : BaseApiController
	{

    /// <summary>
    /// Lists all AtividadeOnboarding.
    /// </summary>
    /// <returns>List of AtividadeOnboardings.</returns>
    [HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AtividadeOnboardingDto>), 200)]
		public async Task<IEnumerable<AtividadeOnboardingDto>> ListAsync()
		{
			var result = await AtividadeOnboardingService.ListAsync();
			return mapper.Map<IEnumerable<AtividadeOnboardingDto>>(result);
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
			var entity = mapper.Map<AtividadeOnboarding>(resource);
			var result = await AtividadeOnboardingService.SaveAsync(entity);

			if (result._message != null)
			{
				result._message.code = HttpStatusCode.BadRequest.ToString();
				return BadRequest(result._message);
			}

			var data = mapper.Map<AtividadeOnboardingDto>(result.Data!);
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
		public async Task<IActionResult> PutAsync(Guid id, [FromBody] AtividadeOnboardingDto resource)
		{
			var AtividadeOnboarding = mapper.Map<AtividadeOnboarding>(resource);
			var result = await AtividadeOnboardingService.UpdateAsync(id, AtividadeOnboarding);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = mapper.Map<AtividadeOnboardingDto>(result.Data!);
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
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var result = await AtividadeOnboardingService.DeleteAsync(id);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = mapper.Map<AtividadeOnboardingDto>(result.Data!);
			return Ok(data);
		}
	}
}