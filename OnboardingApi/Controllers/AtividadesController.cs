using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using System.Net;

namespace OnboardingApi.Controllers
{
	public class AtividadesController(IAtividadeService AtividadeService, IMapper mapper) : BaseApiController
	{

    /// <summary>
    /// Lists all Atividade.
    /// </summary>
    /// <returns>List of Atividades.</returns>
    [HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AtividadeDto>), 200)]
		public async Task<IEnumerable<AtividadeDto>> ListAsync()
		{
			var result = await AtividadeService.ListAsync();
			return mapper.Map<IEnumerable<AtividadeDto>>(result);
		}

		/// <summary>
		/// Saves a new Atividade.
		/// </summary>
		/// <param name="resource">Atividade data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(AtividadeDto), 201)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PostAsync([FromBody] AtividadeDto resource)
		{
			var entity = mapper.Map<Atividade>(resource);
			var result = await AtividadeService.SaveAsync(entity);

			if (result._message != null)
			{
				result._message.code = HttpStatusCode.BadRequest.ToString();
				return BadRequest(result._message);
			}

			var data = mapper.Map<AtividadeDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Updates an existing Atividade according to an identifier.
		/// </summary>
		/// <param name="id">Atividade identifier.</param>
		/// <param name="resource">Updated Atividade data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(AtividadeDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> PutAsync(Guid id, [FromBody] AtividadeDto resource)
		{
			var Atividade = mapper.Map<Atividade>(resource);
			var result = await AtividadeService.UpdateAsync(id, Atividade);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = mapper.Map<AtividadeDto>(result.Data!);
			return Ok(data);
		}

		/// <summary>
		/// Deletes a given Atividade according to an identifier.
		/// </summary>
		/// <param name="id">Atividade identifier.</param>
		/// <returns>Response for the request.</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(AtividadeDto), 200)]
		[ProducesResponseType(typeof(ErrorMessage), 400)]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var result = await AtividadeService.DeleteAsync(id);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = mapper.Map<AtividadeDto>(result.Data!);
			return Ok(data);
		}
	}
}