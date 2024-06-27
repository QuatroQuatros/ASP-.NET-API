﻿using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedUsers()
    {
        // Arrange
        var paginatedResponse = new PaginatedResponse<UserViewModelResponse>
        {
            Items = new List<UserViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };

        _mockUserService.Setup(service => service.GetUsersPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);

        // Act
        var result = await _controller.GetAll(1, 10);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<UserViewModelResponse>>>(okResult.Value);
        Assert.Equal("Usuários recuperados com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithUser()
    {
        // Arrange
        var userResponse = new UserViewModelResponse { Id = 1, Name = "Test User" };
        _mockUserService.Setup(service => service.GetUserByIdAsync(1))
            .ReturnsAsync(userResponse);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<UserViewModelResponse>>(okResult.Value);
        Assert.Equal("Usuário recuperado com sucesso.", response.Message);
        Assert.Equal(userResponse, response.Data);
    }

    [Fact]
    public async Task Create_ReturnsOkResult_WithCreatedUser()
    {
        // Arrange
        var userViewModel = new UserViewModel { Name = "New User", Email = "newuser@example.com", Password = "password" };
        var userResponse = new UserViewModelResponse { Id = 1, Name = "New User" };

        _mockUserService.Setup(service => service.RegisterUserAsync(userViewModel))
            .ReturnsAsync(userResponse);

        // Act
        var result = await _controller.Create(userViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<UserViewModelResponse>>(okResult.Value);
        Assert.Equal("Usuário registrado com sucesso.", response.Message);
        Assert.Equal(userResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedUser()
    {
        // Arrange
        var userViewModelUpdate = new UserViewModelUpdate { Name = "Updated User", Email = "updateduser@example.com", Password = "newpassword" };
        var userResponse = new UserViewModelResponse { Id = 1, Name = "Updated User" };

        _mockUserService.Setup(service => service.UpdateUserAsync(1, userViewModelUpdate))
            .ReturnsAsync(userResponse);

        // Act
        var result = await _controller.Update(1, userViewModelUpdate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<UserViewModelResponse>>(okResult.Value);
        Assert.Equal("Usuário atualizado com sucesso.", response.Message);
        Assert.Equal(userResponse, response.Data);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        // Arrange
        _mockUserService.Setup(service => service.DeleteUserAsync(1))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}