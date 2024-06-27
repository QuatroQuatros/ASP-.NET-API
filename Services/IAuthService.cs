using GestaoDeResiduos.Models;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;

namespace GestaoDeResiduos.Services
{
    public interface IAuthService
    {
        Task<UserViewModelResponse> RegisterUserAsync(UserViewModel userViewModel);
        Task<LoginViewModelResponse> LoginUserAsync(UserViewModel userViewModel);

        string GenerateToken(string username);
    }
}
