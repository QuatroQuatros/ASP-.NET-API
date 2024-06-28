using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class StreetControllerTests
{
    private readonly Mock<IStreetService> _mockStreetService;
    private readonly StreetController _controller;

    public StreetControllerTests()
    {
        _mockStreetService = new Mock<IStreetService>();
        _controller = new StreetController(_mockStreetService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedStreets()
    {
        var paginatedResponse = new PaginatedResponse<StreetViewModelResponse>
        {
            Items = new List<StreetViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };
        _mockStreetService.Setup(service => service.GetPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);
        
        var result = await _controller.GetAll(new Pagination { Page = 1, Size = 10 });
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<StreetViewModelResponse>>>(okResult.Value);
        Assert.Equal("Ruas recuperadas com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithStreet()
    {
        var streetResponse = new StreetViewModelResponse { Id = 1, Name = "Street 1" };
        _mockStreetService.Setup(service => service.GetByIdAsync(1))
            .ReturnsAsync(streetResponse);
        
        var result = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<StreetViewModelResponse>>(okResult.Value);
        Assert.Equal("Rua recuperada com sucesso.", response.Message);
        Assert.Equal(streetResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenStreetNotFound()
    {
        _mockStreetService.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Rua não encontrada."));

        var result = await _controller.GetById(1);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<StreetViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Rua não encontrada.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithStreet()
    {
        var streetViewModel = new StreetViewModel { Name = "New Street", DistrictId = 1 };
        var streetResponse = new StreetViewModelResponse { Id = 1, Name = "New Street", DistrictId = 1 };
        _mockStreetService.Setup(service => service.CreateAsync(streetViewModel))
            .ReturnsAsync(streetResponse);
        
        var result = await _controller.Create(streetViewModel);
        
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<StreetViewModelResponse>>(createdResult.Value);
        Assert.Equal("Rua registrada com sucesso.", response.Message);
        Assert.Equal(streetResponse, response.Data);
        Assert.Equal($"/api/streets/{streetResponse.Id}", createdResult.Location);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedStreet()
    {
        var streetViewModelUpdate = new StreetViewModelUpdate { Name = "Updated Street", DistrictId = 1 };
        var streetResponse = new StreetViewModelResponse { Id = 1, Name = "Updated Street", DistrictId = 1 };
        _mockStreetService.Setup(service => service.UpdateAsync(1, streetViewModelUpdate))
            .ReturnsAsync(streetResponse);
        
        var result = await _controller.Update(1, streetViewModelUpdate);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<StreetViewModelResponse>>(okResult.Value);
        Assert.Equal("Rua atualizada com sucesso.", response.Message);
        Assert.Equal(streetResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundResult_WhenStreetNotFound()
    {
        var streetViewModelUpdate = new StreetViewModelUpdate { Name = "Updated Street", DistrictId = 1 };
        _mockStreetService.Setup(service => service.UpdateAsync(1, streetViewModelUpdate))
            .ThrowsAsync(new NotFoundException("Rua não encontrada."));
        
        var result = await _controller.Update(1, streetViewModelUpdate);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<StreetViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Rua não encontrada.", response.Message);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        _mockStreetService.Setup(service => service.DeleteAsync(1))
            .Returns(Task.CompletedTask);
        
        var result = await _controller.Delete(1);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundResult_WhenStreetNotFound()
    {
        _mockStreetService.Setup(service => service.DeleteAsync(1))
            .ThrowsAsync(new NotFoundException("Rua não encontrada."));
        
        var result = await _controller.Delete(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<StreetViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Rua não encontrada.", response.Message);
    }
}