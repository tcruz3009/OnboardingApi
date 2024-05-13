using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using System.Net;

namespace OnboardingApi.Controllers
{
	public class TotversController : BaseApiController
	{
		private readonly ITotverService _TotverService;
		private readonly IMapper _mapper;

		public TotversController(ITotverService TotverService, IMapper mapper)
		{
			_TotverService = TotverService;
			_mapper = mapper;
		}

		/// <summary>
		/// Lists all categories.
		/// </summary>
		/// <returns>List os categories.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<TotverDto>), 200)]
		public async Task<IEnumerable<TotverDto>> ListAsync()
		{
			var result = await _TotverService.ListAsync();
			return _mapper.Map<IEnumerable<TotverDto>>(result);
		}

		/// <summary>
		/// Saves a new Totver.
		/// </summary>
		/// <param name="resource">Totver data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(TotverDto), 201)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PostAsync([FromBody] TotverDto resource)
		{
			var entity = _mapper.Map<Totver>(resource);
			var result = await _TotverService.SaveAsync(entity);

			if (result._message != null)
			{
				result._message.code = HttpStatusCode.BadRequest.ToString();
				return BadRequest(result._message);
			}

			var data = _mapper.Map<TotverDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Updates an existing Totver according to an identifier.
		/// </summary>
		/// <param name="id">Totver identifier.</param>
		/// <param name="resource">Updated Totver data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(TotverDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PutAsync(int id, [FromBody] TotverDto resource)
		{
			var totver = _mapper.Map<Totver>(resource);
			var result = await _TotverService.UpdateAsync(id, totver);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<TotverDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Deletes a given Totver according to an identifier.
		/// </summary>
		/// <param name="id">Totver identifier.</param>
		/// <returns>Response for the request.</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(TotverDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var result = await _TotverService.DeleteAsync(id);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<TotverDto>(result.Data!);
			return Ok(data);
		}
	}
}