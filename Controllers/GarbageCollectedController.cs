using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Repositories.Impl;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers;

[ApiController]
[Route("api/garbage-collected")]
[Authorize]
public class GarbageCollectedController : Controller
{
private readonly IGarbageCollectedService _garbageCollectedService;

    public GarbageCollectedController(IGarbageCollectedService garbageCollectedService)
    {
        _garbageCollectedService = garbageCollectedService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        if (pagination.isInvalid())
        {
            return BadRequest(new BaseApiResponse<PaginatedResponse<GarbageCollectedViewModelResponse>>("Página ou tamanho da página inválidos.", null));
        }
        var paginatedResults = await _garbageCollectedService.GetPaginatedAsync(pagination.Page, pagination.Size);
        return Ok(new BaseApiResponse<PaginatedResponse<GarbageCollectedViewModelResponse>>("Busca de lixo coletados realizada com sucesso.", paginatedResults));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var district = await _garbageCollectedService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<GarbageCollectedViewModelResponse>("Busca de lixo coletado realizada com sucesso.", district));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<GarbageCollectedViewModelResponse>("Lixo coletado não encontrado.", null));
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GarbageCollectedViewModel request)
    {
        var garbageCollected = await _garbageCollectedService.CreateAsync(request);
        return Created($"/api/garbage-collected/{garbageCollected.Id}", new BaseApiResponse<GarbageCollectedViewModelResponse>("Lixo coletado registrado com sucesso.", garbageCollected));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GarbageCollectedViewModelUpdate request)
    {
        try
        {
            var garbageCollected = await _garbageCollectedService.UpdateAsync(id, request);
            return Ok(new BaseApiResponse<GarbageCollectedViewModelResponse>("Lixo coletado atualizado com sucesso.", garbageCollected));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<GarbageCollectedViewModelResponse>("Lixo coletado não encontrado.", null));
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _garbageCollectedService.DeleteAsync(id);
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<GarbageCollectedViewModelResponse>("Lixo coletado não encontrado.", null));
        }
       
    }
    
    [HttpGet("state-more-trash")]
    public async Task<IActionResult> GetStateMoreTrashAsync([FromQuery] int? state, [FromQuery] int? collectionTypeId)
    {
        var result = await _garbageCollectedService.GetStateMoreTrashAsync(state, collectionTypeId);
        return Ok(new BaseApiResponse<TrashResultState>("Estado com mais lixo recuperado com sucesso.", result));
    }
    
    [HttpGet("region-more-trash")]
    public async Task<IActionResult> GetRegionMoreTrashAsync([FromQuery] int? region, [FromQuery] int? collectionTypeId)
    {
        var result = await _garbageCollectedService.GetRegionMoreTrashAsync(region, collectionTypeId);
        return Ok(new BaseApiResponse<TrashResultRegion>("Região com mais lixo recuperado com sucesso.", result));
    }
    
    [HttpGet("neighborhood-more-trash")]
    public async Task<IActionResult> GetNeighborhoodMoreTrashAsync([FromQuery] int? district, [FromQuery] int? collectionTypeId)
    {
        var result = await _garbageCollectedService.GetNeighborhoodMoreTrashAsync(district, collectionTypeId);
        return Ok(new BaseApiResponse<TrashResultNeighborhood>("Bairro com mais lixo recuperado com sucesso.", result));
    }
}