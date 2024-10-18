using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers;


[ApiController]
[Route("api/streets")]
[Authorize]
public class StreetController : Controller
{
    private readonly IStreetService _streetService;

    public StreetController(IStreetService streetService)
    {
        _streetService = streetService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        if (pagination.isInvalid())
        {
            return BadRequest(new BaseApiResponse<PaginatedResponse<StateViewModelResponse>>("Página ou tamanho da página inválidos.", null));
        }
        var paginatedResults = await _streetService.GetPaginatedAsync(pagination.Page, pagination.Size);
        return Ok(new BaseApiResponse<PaginatedResponse<StreetViewModelResponse>>("Ruas recuperadas com sucesso.", paginatedResults));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var street = await _streetService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<StreetViewModelResponse>("Rua recuperada com sucesso.", street));
        }catch (NotFoundException)
        {
            return NotFound(new BaseApiResponse<StreetViewModelResponse>("Rua não encontrada.", null));
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StreetViewModel request)
    {
        var street = await _streetService.CreateAsync(request);
        return Created($"/api/streets/{street.Id}", new BaseApiResponse<StreetViewModelResponse>("Rua registrada com sucesso.", street));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StreetViewModelUpdate request)
    {
        try
        {
            var street = await _streetService.UpdateAsync(id, request);
            return Ok(new BaseApiResponse<StreetViewModelResponse>("Rua atualizada com sucesso.", street));
        }catch (NotFoundException)
        {
            return NotFound(new BaseApiResponse<StreetViewModelResponse>("Rua não encontrada.", null));
        }
       
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _streetService.DeleteAsync(id);
            return NoContent();
        }catch (NotFoundException)
        {
            return NotFound(new BaseApiResponse<StreetViewModelResponse>("Rua não encontrada.", null));
        }

    }
}