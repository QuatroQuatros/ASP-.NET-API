using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class GarbageCollectionTypeControllerTests
{
    private readonly Mock<IGarbageCollectionTypeService> _mockGarbageCollectionTypeService;
    private readonly GarbageCollectionTypeController _controller;

    public GarbageCollectionTypeControllerTests()
    {
        _mockGarbageCollectionTypeService = new Mock<IGarbageCollectionTypeService>();
        _controller = new GarbageCollectionTypeController(_mockGarbageCollectionTypeService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedGarbageCollectionTypes()
    {
        var paginatedResponse = new PaginatedResponse<GarbageCollectionTypeViewModelResponse>
        {
            Items = new List<GarbageCollectionTypeViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };
        _mockGarbageCollectionTypeService.Setup(service => service.GetPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);

        var result = await _controller.GetAll(new Pagination { Page = 1, Size = 10 });

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<GarbageCollectionTypeViewModelResponse>>>(okResult.Value);
        Assert.Equal("Tipos de coleta recuperados com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithGarbageCollectionType()
    {
        var garbageCollectionTypeResponse = new GarbageCollectionTypeViewModelResponse { Id = 1, Name = "Type 1" };
        _mockGarbageCollectionTypeService.Setup(service => service.GetByIdAsync(1))
            .ReturnsAsync(garbageCollectionTypeResponse);

        var result = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectionTypeViewModelResponse>>(okResult.Value);
        Assert.Equal("Tipo de coleta recuperado com sucesso.", response.Message);
        Assert.Equal(garbageCollectionTypeResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenGarbageCollectionTypeNotFound()
    {
        _mockGarbageCollectionTypeService.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Tipo de coleta não encontrado."));

        var result = await _controller.GetById(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectionTypeViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Tipo de coleta não encontrado.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithGarbageCollectionType()
    {
        var garbageCollectionTypeViewModel = new GarbageCollectionTypeViewModel { Name = "New Type" };
        var garbageCollectionTypeResponse = new GarbageCollectionTypeViewModelResponse { Id = 1, Name = "New Type" };
        _mockGarbageCollectionTypeService.Setup(service => service.CreateAsync(garbageCollectionTypeViewModel))
            .ReturnsAsync(garbageCollectionTypeResponse);

        var result = await _controller.Create(garbageCollectionTypeViewModel);

        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectionTypeViewModelResponse>>(createdResult.Value);
        Assert.Equal("Tipo de coleta registrada com sucesso.", response.Message);
        Assert.Equal(garbageCollectionTypeResponse, response.Data);
        Assert.Equal($"/api/garbage-collection-types/{garbageCollectionTypeResponse.Id}", createdResult.Location);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedGarbageCollectionType()
    {
        var garbageCollectionTypeViewModelUpdate = new GarbageCollectionTypeViewModelUpdate { Name = "Updated Type" };
        var garbageCollectionTypeResponse = new GarbageCollectionTypeViewModelResponse { Id = 1, Name = "Updated Type" };
        _mockGarbageCollectionTypeService.Setup(service => service.UpdateAsync(1, garbageCollectionTypeViewModelUpdate))
            .ReturnsAsync(garbageCollectionTypeResponse);

        var result = await _controller.Update(1, garbageCollectionTypeViewModelUpdate);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectionTypeViewModelResponse>>(okResult.Value);
        Assert.Equal("Tipo de coleta atualizada com sucesso.", response.Message);
        Assert.Equal(garbageCollectionTypeResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundResult_WhenGarbageCollectionTypeNotFound()
    {
        var garbageCollectionTypeViewModelUpdate = new GarbageCollectionTypeViewModelUpdate { Name = "Updated Type" };
        _mockGarbageCollectionTypeService.Setup(service => service.UpdateAsync(1, garbageCollectionTypeViewModelUpdate))
            .ThrowsAsync(new NotFoundException("Tipo de coleta não encontrado."));

        var result = await _controller.Update(1, garbageCollectionTypeViewModelUpdate);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectionTypeViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Tipo de coleta não encontrado.", response.Message);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        _mockGarbageCollectionTypeService.Setup(service => service.DeleteAsync(1))
            .Returns(Task.CompletedTask);

        var result = await _controller.Delete(1);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundResult_WhenGarbageCollectionTypeNotFound()
    {
        _mockGarbageCollectionTypeService.Setup(service => service.DeleteAsync(1))
            .ThrowsAsync(new NotFoundException("Tipo de coleta não encontrado."));

        var result = await _controller.Delete(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<GarbageCollectionTypeViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Tipo de coleta não encontrado.", response.Message);
    }
}