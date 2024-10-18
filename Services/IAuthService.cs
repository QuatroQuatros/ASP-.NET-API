using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;

namespace GestaoDeResiduos.Services
{
    public interface IAuthService
    {
        Task<UserViewModelResponse> RegisterUserAsync(UserViewModel userViewModel);
        Task<LoginViewModelResponse> LoginUserAsync(UserViewModel userViewModel);

        string GenerateToken(string username);
    }
}
