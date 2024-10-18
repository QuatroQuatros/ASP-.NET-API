using AutoMapper;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services.Impl;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<UserViewModelResponse>> GetUsersPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _userRepository.GetUsersPaginatedAsync(pageNumber, pageSize);
    }
    
    public async Task<UserViewModelResponse> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("Usuário não encontrado.");
        }
        
        var response = _mapper.Map<UserViewModelResponse>(user);
        return response;
    }
    
    public async Task<UserViewModelResponse> RegisterUserAsync(UserViewModel userViewModel)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(userViewModel.Email);
        if (existingUser != null)
        {
            throw new Exception("Usuário já cadastrado.");
        }

        var user = _mapper.Map<UserModel>(userViewModel);
        user.Password = BCrypt.Net.BCrypt.HashPassword(userViewModel.Password);
        var newUser = await _userRepository.AddUserAsync(user);

        return _mapper.Map<UserViewModelResponse>(newUser);
    }
    
    public async Task<UserViewModelResponse> UpdateUserAsync(int id, UserViewModelUpdate request)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("Usuário não encontrado.");
        }
        
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null && existingUser.Id != id)
        {
            throw new ConflictException("O e-mail informado já está em uso.");
        }

        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        user.BirthDate = request.BirthDate ?? DateTime.MinValue;

        var userModel = _mapper.Map<UserModel>(user);
        await _userRepository.UpdateUserAsync(userModel);
        return _mapper.Map<UserViewModelResponse>(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("Usuário não encontrado.");
        }
        var userModel = _mapper.Map<UserModel>(user);
        await _userRepository.DeleteUserAsync(userModel);
    }
}