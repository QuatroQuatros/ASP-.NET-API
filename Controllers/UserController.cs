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

    [AllowAnonymous]
    [HttpGet("teste")]
    public ActionResult<String> Teste()
    {
        return Ok(new
        {
            message = "Batata"
        });
        
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]int page = 1, [FromQuery]int size = 10)
    {
        var paginatedUsers = await _userService.GetUsersPaginatedAsync(page, size);
        return Ok(new BaseApiResponse<PaginatedResponse<UserViewModelResponse>>("Usuários recuperados com sucesso.", paginatedUsers));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(new BaseApiResponse<UserViewModelResponse>("Usuário recuperado com sucesso.", user));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserViewModel request)
    {
        var user = await _userService.RegisterUserAsync(request);
        return Ok(new BaseApiResponse<UserViewModelResponse>("Usuário registrado com sucesso.", user));
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserViewModelUpdate request)
    {
        var user = await _userService.UpdateUserAsync(id, request);
        return Ok(new BaseApiResponse<UserViewModelResponse>("Usuário atualizado com sucesso.", user));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
   
}

