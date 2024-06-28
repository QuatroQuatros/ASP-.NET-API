using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeResiduos.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    //[AllowAnonymous]
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        if (pagination.isInvalid())
        {
            return BadRequest(new BaseApiResponse<PaginatedResponse<UserViewModelResponse>>("Página ou tamanho da página inválidos.", null));
        }
        var paginatedResults = await _userService.GetUsersPaginatedAsync(pagination.Page, pagination.Size);
        return Ok(new BaseApiResponse<PaginatedResponse<UserViewModelResponse>>("Usuários recuperados com sucesso.", paginatedResults));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(new BaseApiResponse<UserViewModelResponse>("Usuário recuperado com sucesso.", user));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<UserViewModelResponse>("Usuário não encontrado.", null));
        }
        
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserViewModel request)
    {
        var user = await _userService.RegisterUserAsync(request);
        return Created($"/api/users/{user.Id}", new BaseApiResponse<UserViewModelResponse>("Usuário registrado com sucesso.", user));
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserViewModelUpdate request)
    {
        try
        {
            var user = await _userService.UpdateUserAsync(id, request);
            return Ok(new BaseApiResponse<UserViewModelResponse>("Usuário atualizado com sucesso.", user));
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<UserViewModelResponse>("Usuário não encontrado.", null));
        }
       
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }catch (NotFoundException e)
        {
            return NotFound(new BaseApiResponse<UserViewModelResponse>("Usuário não encontrado.", null));
        }

    }
   
}

