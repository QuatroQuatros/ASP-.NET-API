using AutoMapper;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestaoDeResiduos.Responses;

namespace GestaoDeResiduos.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public AuthService(IConfiguration configuration, IUserRepository userRepository, IMapper mapper)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mapper = mapper;
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
        
        public async Task<LoginViewModelResponse> LoginUserAsync(UserViewModel userViewModel)
        {
            var user = await _userRepository.GetUserByEmailAsync(userViewModel.Email);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(userViewModel.Password, user.Password))
            {
                throw new Exception("Senha inválida");
            }

            var token = this.GenerateToken(user.Email);
            var userResponse = new UserViewModelResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Role = user.Role
            };

            return new LoginViewModelResponse(userResponse, token);
        }

        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "4444",
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
