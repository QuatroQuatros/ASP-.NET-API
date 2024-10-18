﻿using GestaoDeResiduos.Models;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;

namespace GestaoDeResiduos.Repositories
{
    public interface IUserRepository
    {
        Task<PaginatedResponse<UserViewModelResponse>> GetUsersPaginatedAsync(int pageNumber, int pageSize);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByEmailAsync(string? email);
        Task<UserModel> AddUserAsync(UserModel user);
        
        Task<UserModel> UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(UserModel user);

        

    }
}
