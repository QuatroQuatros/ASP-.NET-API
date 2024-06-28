using GestaoDeResiduos.Exceptions;
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
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        if (pagination.isInvalid())
        {
            return BadRequest(new BaseApiResponse<PaginatedResponse<RegionViewModelResponse>>("Página ou tamanho da página inválidos.", null));
        }
        var paginatedResult = await _regionService.GetPaginatedAsync(pagination.Page, pagination.Size);
        return Ok(new BaseApiResponse<PaginatedResponse<RegionViewModelResponse>>("Regiões recuperadas com sucesso.", paginatedResult));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var region = await _regionService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<RegionViewModelResponse>("Região recuperada com sucesso.", region));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<RegionViewModelResponse>("Região não encontrada.", null));
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegionViewModel request)
    {
        var region = await _regionService.CreateAsync(request);
        return Created($"/api/regions/{region.Id}", new BaseApiResponse<RegionViewModelResponse>("Região registrada com sucesso.", region));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RegionViewModelUpdate request)
    {
        try
        {
            var region = await _regionService.UpdateAsync(id, request);
            return Ok(new BaseApiResponse<RegionViewModelResponse>("Região atualizada com sucesso.", region));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<RegionViewModelResponse>("Região não encontrada.", null));
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _regionService.DeleteAsync(id);
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<RegionViewModelResponse>("Região não encontrada.", null));
        }
        
    }
}