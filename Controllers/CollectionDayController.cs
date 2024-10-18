using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers;


[ApiController]
[Route("api/collection-days")]
[Authorize]
public class CollectionDayController : Controller
{
    private readonly ICollectionDayService _collectionDayService;

    public CollectionDayController(ICollectionDayService collectionDayService)
    {
        _collectionDayService = collectionDayService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        if (pagination.isInvalid())
        {
            return BadRequest(new BaseApiResponse<PaginatedResponse<CollectionDayViewModelResponse>>("Página ou tamanho da página inválidos.", null));
        }
        var paginatedResult = await _collectionDayService.GetPaginatedAsync(pagination.Page, pagination.Size);
        return Ok(new BaseApiResponse<PaginatedResponse<CollectionDayViewModelResponse>>("Agendamentos recuperados com sucesso.", paginatedResult));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var collectionDay = await _collectionDayService.GetByIdAsync(id);
            return Ok(new BaseApiResponse<CollectionDayViewModelResponse>("Agendamento recuperado com sucesso.", collectionDay));
        }catch (NotFoundException)
        {
            return NotFound(new BaseApiResponse<CollectionDayViewModelResponse>("Agendamento não encontrado.", null));
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CollectionDayViewModel request)
    {
        
        var collectionDay = await _collectionDayService.CreateAsync(request);
        return Created($"/api/collection-days/{collectionDay.Id}", new BaseApiResponse<CollectionDayViewModelResponse>("Tipo de coleta registrada com sucesso.", collectionDay));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CollectionDayViewModelUpdate request)
    {
        try
        {
            var collectionDay = await _collectionDayService.UpdateAsync(id, request);
            return Ok(new BaseApiResponse<CollectionDayViewModelResponse>("Agendamento atualizao com sucesso.", collectionDay));
        }catch (NotFoundException)
        {
            return NotFound(new BaseApiResponse<CollectionDayViewModelResponse>("Agendamento não encontrado.", null));
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _collectionDayService.DeleteAsync(id);
            return NoContent();
        }catch (NotFoundException)
        {
            return NotFound(new BaseApiResponse<CollectionDayViewModelResponse>("Agendamento não encontrado.", null));
        }
        
    }
    
    [HttpPut("{id}/finish")]
    public async Task<IActionResult> MarkAsComplete([FromRoute] int id)
    {
        var response = await _collectionDayService.MarkAsCompleteAsync(id);
        return Ok(new BaseApiResponse<CollectionDayViewModelResponse>("Coleta finalizada com sucesso.", response));
         
    }
    
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> MarkAsCanceledAsync([FromRoute] int id)
    {
        var response = await _collectionDayService.MarkAsCanceledAsync(id);
        return Ok(new BaseApiResponse<CollectionDayViewModelResponse>("Coleta cancelada com sucesso.", response));
         
    }
    
    [HttpPut("{id}/start")]
    public async Task<IActionResult> MarkAsInProgressAsync([FromRoute] int id)
    {
        var response = await _collectionDayService.MarkAsInProgressAsync(id);
        return Ok(new BaseApiResponse<CollectionDayViewModelResponse>("Coleta iniciada com sucesso.", response));
         
    }
}