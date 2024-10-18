using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class CollectionDayControllerTests
{
    private readonly Mock<ICollectionDayService> _mockCollectionDayService;
    private readonly CollectionDayController _controller;

    public CollectionDayControllerTests()
    {
        _mockCollectionDayService = new Mock<ICollectionDayService>();
        _controller = new CollectionDayController(_mockCollectionDayService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedCollectionDays()
    {
        var paginatedResponse = new PaginatedResponse<CollectionDayViewModelResponse>
        {
            Items = new List<CollectionDayViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };
        _mockCollectionDayService.Setup(service => service.GetPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);

        var result = await _controller.GetAll(new Pagination { Page = 1, Size = 10 });

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<CollectionDayViewModelResponse>>>(okResult.Value);
        Assert.Equal("Agendamentos recuperados com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithCollectionDay()
    {
        var collectionDayResponse = new CollectionDayViewModelResponse { Id = 1, StreetId = 1, GarbageCollectionTypeId = 1 };
        _mockCollectionDayService.Setup(service => service.GetByIdAsync(1))
            .ReturnsAsync(collectionDayResponse);

        var result = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(okResult.Value);
        Assert.Equal("Agendamento recuperado com sucesso.", response.Message);
        Assert.Equal(collectionDayResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenCollectionDayNotFound()
    {
        _mockCollectionDayService.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Agendamento não encontrado."));

        var result = await _controller.GetById(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Agendamento não encontrado.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithCollectionDay()
    {
        var collectionDayViewModel = new CollectionDayViewModel { StreetId = 1, GarbageCollectionTypeId = 1 };
        var collectionDayResponse = new CollectionDayViewModelResponse { Id = 1, StreetId = 1, GarbageCollectionTypeId = 1 };
        _mockCollectionDayService.Setup(service => service.CreateAsync(collectionDayViewModel))
            .ReturnsAsync(collectionDayResponse);

        var result = await _controller.Create(collectionDayViewModel);

        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(createdResult.Value);
        Assert.Equal("Tipo de coleta registrada com sucesso.", response.Message);
        Assert.Equal(collectionDayResponse, response.Data);
        Assert.Equal($"/api/collection-days/{collectionDayResponse.Id}", createdResult.Location);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedCollectionDay()
    {
        var collectionDayViewModelUpdate = new CollectionDayViewModelUpdate { StreetId = 1, GarbageCollectionTypeId = 1 };
        var collectionDayResponse = new CollectionDayViewModelResponse { Id = 1, StreetId = 1, GarbageCollectionTypeId = 1 };
        _mockCollectionDayService.Setup(service => service.UpdateAsync(1, collectionDayViewModelUpdate))
            .ReturnsAsync(collectionDayResponse);

        var result = await _controller.Update(1, collectionDayViewModelUpdate);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(okResult.Value);
        Assert.Equal("Agendamento atualizao com sucesso.", response.Message);
        Assert.Equal(collectionDayResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundResult_WhenCollectionDayNotFound()
    {
        var collectionDayViewModelUpdate = new CollectionDayViewModelUpdate { StreetId = 1, GarbageCollectionTypeId = 1 };
        _mockCollectionDayService.Setup(service => service.UpdateAsync(1, collectionDayViewModelUpdate))
            .ThrowsAsync(new NotFoundException("Agendamento não encontrado."));

        var result = await _controller.Update(1, collectionDayViewModelUpdate);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Agendamento não encontrado.", response.Message);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        _mockCollectionDayService.Setup(service => service.DeleteAsync(1))
            .Returns(Task.CompletedTask);

        var result = await _controller.Delete(1);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundResult_WhenCollectionDayNotFound()
    {
        _mockCollectionDayService.Setup(service => service.DeleteAsync(1))
            .ThrowsAsync(new NotFoundException("Agendamento não encontrado."));

        var result = await _controller.Delete(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Agendamento não encontrado.", response.Message);
    }

    [Fact]
    public async Task MarkAsComplete_ReturnsOkResult_WithCollectionDay()
    {
        var collectionDayResponse = new CollectionDayViewModelResponse { Id = 1, StreetId = 1, GarbageCollectionTypeId = 1, Status = CollectionStatus.Coletado.ToString() };
        _mockCollectionDayService.Setup(service => service.MarkAsCompleteAsync(1))
            .ReturnsAsync(collectionDayResponse);

        var result = await _controller.MarkAsComplete(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(okResult.Value);
        Assert.Equal("Coleta finalizada com sucesso.", response.Message);
        Assert.Equal(collectionDayResponse, response.Data);
    }

    [Fact]
    public async Task MarkAsCanceled_ReturnsOkResult_WithCollectionDay()
    {
        var collectionDayResponse = new CollectionDayViewModelResponse { Id = 1, StreetId = 1, GarbageCollectionTypeId = 1, Status = CollectionStatus.Cancelado.ToString() };
        _mockCollectionDayService.Setup(service => service.MarkAsCanceledAsync(1))
            .ReturnsAsync(collectionDayResponse);

        var result = await _controller.MarkAsCanceledAsync(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(okResult.Value);
        Assert.Equal("Coleta cancelada com sucesso.", response.Message);
        Assert.Equal(collectionDayResponse, response.Data);
    }

    [Fact]
    public async Task MarkAsInProgress_ReturnsOkResult_WithCollectionDay()
    {
        var collectionDayResponse = new CollectionDayViewModelResponse { Id = 1, StreetId = 1, GarbageCollectionTypeId = 1, Status = CollectionStatus.EmAndamento.ToString() };
        _mockCollectionDayService.Setup(service => service.MarkAsInProgressAsync(1))
            .ReturnsAsync(collectionDayResponse);

        var result = await _controller.MarkAsInProgressAsync(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<CollectionDayViewModelResponse>>(okResult.Value);
        Assert.Equal("Coleta iniciada com sucesso.", response.Message);
        Assert.Equal(collectionDayResponse, response.Data);
    }
}