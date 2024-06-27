using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers;

[ApiController]
[Route("api/garbage-collection-types")]
[Authorize]
public class GarbageCollectionTypeController : Controller
{
    private readonly IGarbageCollectionTypeService _garbageCollectionTypeService;

    public GarbageCollectionTypeController(IGarbageCollectionTypeService garbageCollectionTypeService)
    {
        _garbageCollectionTypeService = garbageCollectionTypeService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        if (pagination.isInvalid())
        {
            return BadRequest(new BaseApiResponse<PaginatedResponse<GarbageCollectionTypeViewModelResponse>>("Página ou tamanho da página inválidos.", null));
        }
        var paginatedResult = await _garbageCollectionTypeService.GetPaginatedAsync(pagination.Page, pagination.Size);
        return Ok(new BaseApiResponse<PaginatedResponse<GarbageCollectionTypeViewModelResponse>>("Tipos de coleta recuperados com sucesso.", paginatedResult));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var garbageCollectionType = await _garbageCollectionTypeService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<GarbageCollectionTypeViewModelResponse>("Tipo de coleta recuperado com sucesso.", garbageCollectionType));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<GarbageCollectionTypeViewModelResponse>("Tipo de coleta não encontrado.", null));
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GarbageCollectionTypeViewModel request)
    {
        
        var garbageCollectionType = await _garbageCollectionTypeService.CreateAsync(request);
        return Created($"/api/garbage-collection-types/{garbageCollectionType.Id}", new BaseApiResponse<GarbageCollectionTypeViewModelResponse>("Tipo de coleta registrada com sucesso.", garbageCollectionType));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GarbageCollectionTypeViewModelUpdate request)
    {
        try
        {
            var garbageCollectionType = await _garbageCollectionTypeService.UpdateAsync(id, request);
            return Ok(new BaseApiResponse<GarbageCollectionTypeViewModelResponse>("Tipo de coleta atualizada com sucesso.", garbageCollectionType));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<GarbageCollectionTypeViewModelResponse>("Tipo de coleta não encontrado.", null));
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _garbageCollectionTypeService.DeleteAsync(id);
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<GarbageCollectionTypeViewModelResponse>("Tipo de coleta não encontrado.", null));
        }
        
    }
}