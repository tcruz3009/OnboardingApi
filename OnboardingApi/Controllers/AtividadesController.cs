using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Services;
using OnboardingApi.Domain.Services.Communication;
using System.Net;

namespace OnboardingApi.Controllers
{
	public class AtividadesController : BaseApiController
	{
		private readonly IAtividadeService _AtividadeService;
		private readonly IMapper _mapper;

		public AtividadesController(IAtividadeService AtividadeService, IMapper mapper)
		{
			_AtividadeService = AtividadeService;
			_mapper = mapper;
		}

		/// <summary>
		/// Lists all categories.
		/// </summary>
		/// <returns>List os categories.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AtividadeDto>), 200)]
		public async Task<IEnumerable<AtividadeDto>> ListAsync()
		{
			var result = await _AtividadeService.ListAsync();
			return _mapper.Map<IEnumerable<AtividadeDto>>(result);
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
			var entity = _mapper.Map<Atividade>(resource);
			var result = await _AtividadeService.SaveAsync(entity);

			if (result._message != null)
			{
				result._message.code = HttpStatusCode.BadRequest.ToString();
				return BadRequest(result._message);
			}

			var data = _mapper.Map<AtividadeDto>(result.Data!);
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
		public async Task<IActionResult> PutAsync(int id, [FromBody] AtividadeDto resource)
		{
			var Atividade = _mapper.Map<Atividade>(resource);
			var result = await _AtividadeService.UpdateAsync(id, Atividade);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<AtividadeDto>(result.Data!);
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
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var result = await _AtividadeService.DeleteAsync(id);

      if (result._message != null)
      {
        result._message.code = HttpStatusCode.BadRequest.ToString();
        return BadRequest(result._message);
      }

      var data = _mapper.Map<AtividadeDto>(result.Data!);
			return Ok(data);
		}
	}
}