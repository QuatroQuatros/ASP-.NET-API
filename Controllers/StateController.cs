using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            if (pagination.isInvalid())
            {
                return BadRequest(new BaseApiResponse<PaginatedResponse<StateViewModelResponse>>("Página ou tamanho da página inválidos.", null));
            }
            var paginatedResults = await _stateService.GetPaginatedAsync(pagination.Page, pagination.Size);
            return Ok(new BaseApiResponse<PaginatedResponse<StateViewModelResponse>>("Estados recuperados com sucesso.", paginatedResults));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var state = await _stateService.GetByIdAsync(id);
                return Ok(new BaseApiResponse<StateViewModelResponse>("Estado recuperado com sucesso.", state));
            }catch (NotFoundException e)
            {
                return NotFound(new BaseApiResponse<StateViewModelResponse>("Estado não encontrado.", null));
            }
           
        }
        
        [HttpGet("uf/{uf}")]
        public async Task<IActionResult> GetByUf([FromRoute] string uf)
        {
            try
            {
                var state = await _stateService.GetByUfAsync(uf);
                return Ok(new BaseApiResponse<StateViewModelResponse>("Estado recuperado com sucesso.", state));
            }catch (NotFoundException e)
            {
                return NotFound(new BaseApiResponse<StateViewModelResponse>("Estado não encontrado.", null));
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StateViewModel request)
        {
            var state = await _stateService.CreateAsync(request);
            return Created($"/api/states/{state.Id}", new BaseApiResponse<StateViewModelResponse>("Estado registrado com sucesso.", state));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StateViewModelUpdate request)
        {
            try
            {
                var state = await _stateService.UpdateAsync(id, request);
                return Ok(new BaseApiResponse<StateViewModelResponse>("Estado atualizado com sucesso.", state));
            }catch (NotFoundException e)
            {
                return NotFound(new BaseApiResponse<StateViewModelResponse>("Estado não encontrado.", null));
            }
          
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _stateService.DeleteAsync(id);
                return NoContent();
            }catch (NotFoundException e)
            {
                return NotFound(new BaseApiResponse<StateViewModelResponse>("Estado não encontrado.", null));
            }

        }
    }
}

