using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using System.Net;

namespace OnboardingApi.Controllers
{
	public class TotversController(ITotverService TotverService, IMapper mapper) : BaseApiController
	{

    /// <summary>
    /// Lists all Totver.
    /// </summary>
    /// <returns>List of Totvers.</returns>
    [HttpGet]
		[ProducesResponseType(typeof(IEnumerable<TotverDto>), 200)]
		public async Task<IEnumerable<TotverDto>> ListAsync()
		{
			var result = await TotverService.ListAsync();
			return mapper.Map<IEnumerable<TotverDto>>(result);
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
			var entity = mapper.Map<Totver>(resource);
			var result = await TotverService.SaveAsync(entity);

			if (result._message != null)
			{
				result._message.code = HttpStatusCode.BadRequest.ToString();
				return BadRequest(result._message);
			}

			var data = mapper.Map<TotverDto>(result.Data!);
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
		public async Task<IActionResult> PutAsync(Guid id, [FromBody] TotverDto resource)
		{
			var totver = mapper.Map<Totver>(resource);
			var result = await TotverService.UpdateAsync(id, totver);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = mapper.Map<TotverDto>(result.Data!);
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
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var result = await TotverService.DeleteAsync(id);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = mapper.Map<TotverDto>(result.Data!);
			return Ok(data);
		}
	}
}