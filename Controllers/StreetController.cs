using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
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
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var paginatedResults = await _streetService.GetPaginatedAsync(page, size);
        return Ok(new BaseApiResponse<PaginatedResponse<StreetViewModelResponse>>("Ruas recuperadas com sucesso.", paginatedResults));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var street = await _streetService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<StreetViewModelResponse>("Rua recuperada com sucesso.", street));
        }catch (NotFoundException e)
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
        var street = await _streetService.UpdateAsync(id, request);
        return Ok(new BaseApiResponse<StreetViewModelResponse>("Rua atualizada com sucesso.", street));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _streetService.DeleteAsync(id);
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<StreetViewModelResponse>("Rua não encontrada.", null));
        }

    }
}