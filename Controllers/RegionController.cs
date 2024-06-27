using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers;


[ApiController]
[Route("api/regions")]
[Authorize]
public class RegionController : Controller
{
    
    private readonly IRegionService _regionService;

    public RegionController(IRegionService regionService)
    {
        _regionService = regionService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var paginatedStates = await _regionService.GetPaginatedAsync(page, size);
        return Ok(new BaseApiResponse<PaginatedResponse<RegionViewModelResponse>>("Regiões recuperadas com sucesso.", paginatedStates));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var state = await _regionService.GetByIdAsync(id);
        return Ok(new BaseApiResponse<RegionViewModelResponse>("Região recuperada com sucesso.", state));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegionViewModel request)
    {
        var state = await _regionService.CreateAsync(request);
        return Ok(new BaseApiResponse<RegionViewModelResponse>("Região registrada com sucesso.", state));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RegionViewModelUpdate request)
    {
        var state = await _regionService.UpdateAsync(id, request);
        return Ok(new BaseApiResponse<RegionViewModelResponse>("Região atualizada com sucesso.", state));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _regionService.DeleteAsync(id);
        return NoContent();
    }
}