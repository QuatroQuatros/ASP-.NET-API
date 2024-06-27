using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers;


[ApiController]
[Route("api/districts")]
[Authorize]
public class DistrictController : Controller
{
    private readonly IDistrictService _districtService;

    public DistrictController(IDistrictService districtService)
    {
        _districtService = districtService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        if (pagination.isInvalid())
        {
            return BadRequest(new BaseApiResponse<PaginatedResponse<DistrictViewModelResponse>>("Página ou tamanho da página inválidos.", null));
        }
        var paginatedResults = await _districtService.GetPaginatedAsync(pagination.Page, pagination.Size);
        return Ok(new BaseApiResponse<PaginatedResponse<DistrictViewModelResponse>>("Bairros recuperados com sucesso.", paginatedResults));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var district = await _districtService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<DistrictViewModelResponse>("Bairro recuperado com sucesso.", district));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<DistrictViewModelResponse>("Bairro não encontrado.", null));
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DistrictViewModel request)
    {
        var district = await _districtService.CreateAsync(request);
        return Created($"/api/districts/{district.Id}", new BaseApiResponse<DistrictViewModelResponse>("Bairro registrada com sucesso.", district));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DistrictViewModelUpdate request)
    {
        var district = await _districtService.UpdateAsync(id, request);
        return Ok(new BaseApiResponse<DistrictViewModelResponse>("Bairro atualizado com sucesso.", district));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _districtService.DeleteAsync(id);
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<DistrictViewModelResponse>("Bairro não encontrado.", null));
        }
       
    }
}