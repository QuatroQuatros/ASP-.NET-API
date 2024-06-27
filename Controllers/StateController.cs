using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers
{
    [ApiController]
    [Route("api/states")]
    [Authorize]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var paginatedStates = await _stateService.GetPaginatedAsync(page, size);
            return Ok(new BaseApiResponse<PaginatedResponse<StateViewModelResponse>>("Estados recuperados com sucesso.", paginatedStates));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var state = await _stateService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<StateViewModelResponse>("Estado recuperado com sucesso.", state));
        }
        
        [HttpGet("uf/{uf}")]
        public async Task<IActionResult> GetByUf([FromRoute] string uf)
        {
            var state = await _stateService.GetByUfAsync(uf);
            return Ok(new BaseApiResponse<StateViewModelResponse>("Estado recuperado com sucesso.", state));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StateViewModel request)
        {
            var state = await _stateService.CreateAsync(request);
            return Ok(new BaseApiResponse<StateViewModelResponse>("Estado registrado com sucesso.", state));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StateViewModelUpdate request)
        {
            var state = await _stateService.UpdateAsync(id, request);
            return Ok(new BaseApiResponse<StateViewModelResponse>("Estado atualizado com sucesso.", state));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _stateService.DeleteAsync(id);
            return NoContent();
        }
    }
}
