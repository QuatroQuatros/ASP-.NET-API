using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class GarbageCollectedControllerTests
{
    private readonly Mock<IGarbageCollectedService> _mockGarbageCollectedService;
    private readonly GarbageCollectedController _controller;

    public GarbageCollectedControllerTests()
    {
        _mockGarbageCollectedService = new Mock<IGarbageCollectedService>();
        _controller = new GarbageCollectedController(_mockGarbageCollectedService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedGarbageCollected()
    {
        var paginatedResponse = new PaginatedResponse<GarbageCollectedViewModelResponse>
        {
            Items = new List<GarbageCollectedViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };

        _mockGarbageCollectedService.Setup(service => service.GetPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);
        
        var result = await _controller.GetAll(new Pagination { Page = 1, Size = 10 });
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<GarbageCollectedViewModelResponse>>>(okResult.Value);
        Assert.Equal("Busca de lixo coletados realizada com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithGarbageCollected()
    {
        var garbageCollectedResponse = new GarbageCollectedViewModelResponse { Id = 1, Amount = 50 };
        _mockGarbageCollectedService.Setup(service => service.GetByIdAsync(1))
            .ReturnsAsync(garbageCollectedResponse);
        
        var result = await _controller.GetById(1);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectedViewModelResponse>>(okResult.Value);
        Assert.Equal("Busca de lixo coletado realizada com sucesso.", response.Message);
        Assert.Equal(garbageCollectedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenGarbageCollectedDoesNotExist()
    {
        _mockGarbageCollectedService.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Lixo coletado não encontrado."));
        
        var result = await _controller.GetById(1);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectedViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Lixo coletado não encontrado.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithCreatedGarbageCollected()
    {
        var garbageCollectedViewModel = new GarbageCollectedViewModel { Amount = 50 };
        var garbageCollectedResponse = new GarbageCollectedViewModelResponse { Id = 1, Amount = 50 };

        _mockGarbageCollectedService.Setup(service => service.CreateAsync(garbageCollectedViewModel))
            .ReturnsAsync(garbageCollectedResponse);
        
        var result = await _controller.Create(garbageCollectedViewModel);
        
        var createdResult = Assert.IsType<CreatedResult>(result);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectedViewModelResponse>>(createdResult.Value);
        Assert.Equal("Lixo coletado registrado com sucesso.", response.Message);
        Assert.Equal(garbageCollectedResponse, response.Data);
        Assert.Equal($"/api/garbage-collected/{garbageCollectedResponse.Id}", createdResult.Location);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedGarbageCollected()
    {
        var garbageCollectedViewModelUpdate = new GarbageCollectedViewModelUpdate { Amount = 60 };
        var garbageCollectedResponse = new GarbageCollectedViewModelResponse { Id = 1, Amount = 60 };

        _mockGarbageCollectedService.Setup(service => service.UpdateAsync(1, garbageCollectedViewModelUpdate))
            .ReturnsAsync(garbageCollectedResponse);
        
        var result = await _controller.Update(1, garbageCollectedViewModelUpdate);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectedViewModelResponse>>(okResult.Value);
        Assert.Equal("Lixo coletado atualizado com sucesso.", response.Message);
        Assert.Equal(garbageCollectedResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenGarbageCollectedDoesNotExist()
    {
        var garbageCollectedViewModelUpdate = new GarbageCollectedViewModelUpdate { Amount = 60 };
        _mockGarbageCollectedService.Setup(service => service.UpdateAsync(1, garbageCollectedViewModelUpdate))
            .ThrowsAsync(new NotFoundException("Lixo coletado não encontrado."));
        
        var result = await _controller.Update(1, garbageCollectedViewModelUpdate);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectedViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Lixo coletado não encontrado.", response.Message);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        _mockGarbageCollectedService.Setup(service => service.DeleteAsync(1))
            .Returns(Task.CompletedTask);
        
        var result = await _controller.Delete(1);
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenGarbageCollectedDoesNotExist()
    {
        _mockGarbageCollectedService.Setup(service => service.DeleteAsync(1))
            .ThrowsAsync(new NotFoundException("Lixo coletado não encontrado."));
        
        var result = await _controller.Delete(1);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectedViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Lixo coletado não encontrado.", response.Message);
    }

    [Fact]
    public async Task GetStateMoreTrashAsync_ReturnsOkResult_WithTrashResultState()
    {
        var trashResultState = new TrashResultState { QuantidadeLixo = 100, NomeEstado = "Estado1" };
        _mockGarbageCollectedService.Setup(service => service.GetStateMoreTrashAsync(null, null))
            .ReturnsAsync(trashResultState);
        
        var result = await _controller.GetStateMoreTrashAsync(null, null);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<TrashResultState>>(okResult.Value);
        Assert.Equal("Estado com mais lixo recuperado com sucesso.", response.Message);
        Assert.Equal(trashResultState, response.Data);
    }

    [Fact]
    public async Task GetRegionMoreTrashAsync_ReturnsOkResult_WithTrashResultRegion()
    {
        var trashResultRegion = new TrashResultRegion { QuantidadeLixo = 100, NomeRegiao = "Região1" };
        _mockGarbageCollectedService.Setup(service => service.GetRegionMoreTrashAsync(null, null))
            .ReturnsAsync(trashResultRegion);
        
        var result = await _controller.GetRegionMoreTrashAsync(null, null);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<TrashResultRegion>>(okResult.Value);
        Assert.Equal("Região com mais lixo recuperado com sucesso.", response.Message);
        Assert.Equal(trashResultRegion, response.Data);
    }
    
    [Fact]
    public async Task GetNeighborhoodMoreTrashAsync_WithParameters_ReturnsOkResult_WithTrashResultNeighborhood()
    {
        var trashResultNeighborhood = new TrashResultNeighborhood { QuantidadeLixo = 150, NomeBairro = "Bairro2" };
        _mockGarbageCollectedService.Setup(service => service.GetNeighborhoodMoreTrashAsync(1, 1))
            .ReturnsAsync(trashResultNeighborhood);

        var result = await _controller.GetNeighborhoodMoreTrashAsync(1, 1);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseApiResponse<TrashResultNeighborhood>>(okResult.Value);
        Assert.Equal("Bairro com mais lixo recuperado com sucesso.", response.Message);
        Assert.Equal(trashResultNeighborhood, response.Data);
    }
}