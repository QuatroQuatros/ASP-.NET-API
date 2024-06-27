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
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var paginatedStates = await _districtService.GetPaginatedAsync(page, size);
        return Ok(new BaseApiResponse<PaginatedResponse<DistrictViewModelResponse>>("Bairros recuperados com sucesso.", paginatedStates));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var state = await _districtService.GetByIdAsync(id);
        return Ok(new BaseApiResponse<DistrictViewModelResponse>("Bairro recuperado com sucesso.", state));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DistrictViewModel request)
    {
        var state = await _districtService.CreateAsync(request);
        return Ok(new BaseApiResponse<DistrictViewModelResponse>("Bairro registrado com sucesso.", state));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DistrictViewModelUpdate request)
    {
        var state = await _districtService.UpdateAsync(id, request);
        return Ok(new BaseApiResponse<DistrictViewModelResponse>("Bairro atualizado com sucesso.", state));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _districtService.DeleteAsync(id);
        return NoContent();
    }
}