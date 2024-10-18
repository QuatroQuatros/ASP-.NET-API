using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services;

public interface IUserService
{
    Task<PaginatedResponse<UserViewModelResponse>> GetUsersPaginatedAsync(int pageNumber, int pageSize);
    Task<UserViewModelResponse> GetUserByIdAsync(int id);
    Task<UserViewModelResponse> RegisterUserAsync(UserViewModel userViewModel);
    
    Task<UserViewModelResponse> UpdateUserAsync(int id, UserViewModelUpdate request);
    Task DeleteUserAsync(int id);
}