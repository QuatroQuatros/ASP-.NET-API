using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

 public class StateControllerTests
{
    private readonly Mock<IStateService> _mockStateService;
    private readonly StateController _controller;

    public StateControllerTests()
    {
        _mockStateService = new Mock<IStateService>();
        _controller = new StateController(_mockStateService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedStates()
    {
        var paginatedResponse = new PaginatedResponse<StateViewModelResponse>
        {
            Items = new List<StateViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };
        _mockStateService.Setup(service => service.GetPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);
        
        var result = await _controller.GetAll(new Pagination { Page = 1, Size = 10 });
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<StateViewModelResponse>>>(okResult.Value);
        Assert.Equal("Estados recuperados com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithState()
    {
        var stateResponse = new StateViewModelResponse { Id = 1, Name = "State 1", UF = "ST" };
        _mockStateService.Setup(service => service.GetByIdAsync(1))
            .ReturnsAsync(stateResponse);
        
        var result = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(okResult.Value);
        Assert.Equal("Estado recuperado com sucesso.", response.Message);
        Assert.Equal(stateResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenStateNotFound()
    {
        _mockStateService.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Estado não encontrado."));

        var result = await _controller.GetById(1);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Estado não encontrado.", response.Message);
    }

    [Fact]
    public async Task GetByUf_ReturnsOkResult_WithState()
    {
        var stateResponse = new StateViewModelResponse { Id = 1, Name = "State 1", UF = "ST" };
        _mockStateService.Setup(service => service.GetByUfAsync("ST"))
            .ReturnsAsync(stateResponse);
        
        var result = await _controller.GetByUf("ST");

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(okResult.Value);
        Assert.Equal("Estado recuperado com sucesso.", response.Message);
        Assert.Equal(stateResponse, response.Data);
    }

    [Fact]
    public async Task GetByUf_ReturnsNotFoundResult_WhenStateNotFound()
    {
        _mockStateService.Setup(service => service.GetByUfAsync("ST"))
            .ThrowsAsync(new NotFoundException("Estado não encontrado."));

        var result = await _controller.GetByUf("ST");

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Estado não encontrado.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithState()
    {
        var stateViewModel = new StateViewModel { Name = "New State", UF = "NS" };
        var stateResponse = new StateViewModelResponse { Id = 1, Name = "New State", UF = "NS" };
        _mockStateService.Setup(service => service.CreateAsync(stateViewModel))
            .ReturnsAsync(stateResponse);
        
        var result = await _controller.Create(stateViewModel);
        
        var createdResult = Assert.IsType<CreatedResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(createdResult.Value);
        Assert.Equal("Estado registrado com sucesso.", response.Message);
        Assert.Equal(stateResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedState()
    {
        var stateViewModelUpdate = new StateViewModelUpdate { Name = "Updated State", UF = "US" };
        var stateResponse = new StateViewModelResponse { Id = 1, Name = "Updated State", UF = "US" };
        _mockStateService.Setup(service => service.UpdateAsync(1, stateViewModelUpdate))
            .ReturnsAsync(stateResponse);
        
        var result = await _controller.Update(1, stateViewModelUpdate);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(okResult.Value);
        Assert.Equal("Estado atualizado com sucesso.", response.Message);
        Assert.Equal(stateResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundResult_WhenStateNotFound()
    {
        var stateViewModelUpdate = new StateViewModelUpdate { Name = "Updated State", UF = "US" };
        _mockStateService.Setup(service => service.UpdateAsync(1, stateViewModelUpdate))
            .ThrowsAsync(new NotFoundException("Estado não encontrado."));
        
        var result = await _controller.Update(1, stateViewModelUpdate);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Estado não encontrado.", response.Message);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        _mockStateService.Setup(service => service.DeleteAsync(1))
            .Returns(Task.CompletedTask);
        
        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundResult_WhenStateNotFound()
    {
        _mockStateService.Setup(service => service.DeleteAsync(1))
            .ThrowsAsync(new NotFoundException("Estado não encontrado."));
        
        var result = await _controller.Delete(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<StateViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Estado não encontrado.", response.Message);
    }
}