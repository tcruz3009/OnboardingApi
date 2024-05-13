using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using System.Net;

namespace OnboardingApi.Controllers
{
	public class OnboardingsController : BaseApiController
	{
		private readonly IOnboardingService _OnboardingService;
		private readonly IMapper _mapper;

		public OnboardingsController(IOnboardingService OnboardingService, IMapper mapper)
		{
			_OnboardingService = OnboardingService;
			_mapper = mapper;
		}

		/// <summary>
		/// Lists all categories.
		/// </summary>
		/// <returns>List os categories.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<OnboardingDto>), 200)]
		public async Task<IEnumerable<OnboardingDto>> ListAsync()
		{
			var result = await _OnboardingService.ListAsync();
			return _mapper.Map<IEnumerable<OnboardingDto>>(result);
		}

		/// <summary>
		/// Saves a new Onboarding.
		/// </summary>
		/// <param name="resource">Onboarding data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(OnboardingDto), 201)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PostAsync([FromBody] OnboardingDto resource)
		{
			var entity = _mapper.Map<Onboarding>(resource);
			var result = await _OnboardingService.SaveAsync(entity);

			if (result._message != null)
			{
				result._message.code = HttpStatusCode.BadRequest.ToString();
				return BadRequest(result._message);
			}

			var data = _mapper.Map<OnboardingDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Updates an existing Onboarding according to an identifier.
		/// </summary>
		/// <param name="id">Onboarding identifier.</param>
		/// <param name="resource">Updated Onboarding data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(OnboardingDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PutAsync(int id, [FromBody] OnboardingDto resource)
		{
			var Onboarding = _mapper.Map<Onboarding>(resource);
			var result = await _OnboardingService.UpdateAsync(id, Onboarding);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<OnboardingDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Deletes a given Onboarding according to an identifier.
		/// </summary>
		/// <param name="id">Onboarding identifier.</param>
		/// <returns>Response for the request.</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(OnboardingDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var result = await _OnboardingService.DeleteAsync(id);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<OnboardingDto>(result.Data!);
			return Ok(data);
		}
	}
}