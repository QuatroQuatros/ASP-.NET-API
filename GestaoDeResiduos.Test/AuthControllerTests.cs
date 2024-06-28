using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new AuthController(_mockAuthService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Register_ReturnsOkResult_WithUser()
    {
        var userViewModel = new UserViewModel { Name = "New User", Email = "newuser@example.com", Password = "password" };
        var userResponse = new UserViewModelResponse { Id = 1, Name = "New User" };
        _mockAuthService.Setup(service => service.RegisterUserAsync(userViewModel))
            .ReturnsAsync(userResponse);

        var result = await _controller.Register(userViewModel);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<UserViewModelResponse>>(okResult.Value);
        Assert.Equal("Usuário registrado com sucesso.", response.Message);
        Assert.Equal(userResponse, response.Data);
    }

    [Fact]
    public async Task Login_ReturnsOkResult_WithLoginResponse()
    {
        var loginViewModel = new LoginViewModel { Email = "testuser@example.com", Password = "password" };
        var userViewModel = new UserViewModel { Email = "testuser@example.com", Password = "password" };
        var userResponse = new UserViewModelResponse { Id = 1, Name = "Test User", Email = "testuser@example.com" };
        var loginResponse = new LoginViewModelResponse(userResponse, "jwt-token");

        _mockMapper.Setup(m => m.Map<UserViewModel>(loginViewModel)).Returns(userViewModel);
        _mockAuthService.Setup(service => service.LoginUserAsync(userViewModel))
            .ReturnsAsync(loginResponse);

        var result = await _controller.Login(loginViewModel);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<LoginViewModelResponse>>(okResult.Value);
        Assert.Equal("Login realizado com sucesso.", response.Message);
        Assert.Equal(loginResponse, response.Data);
    }
}