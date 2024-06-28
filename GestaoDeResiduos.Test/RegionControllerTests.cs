using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class RegionControllerTests
{
    private readonly Mock<IRegionService> _mockRegionService;
    private readonly RegionController _controller;

    public RegionControllerTests()
    {
        _mockRegionService = new Mock<IRegionService>();
        _controller = new RegionController(_mockRegionService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedRegions()
    {
        var paginatedResponse = new PaginatedResponse<RegionViewModelResponse>
        {
            Items = new List<RegionViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };
        _mockRegionService.Setup(service => service.GetPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);
        
        var result = await _controller.GetAll(new Pagination { Page = 1, Size = 10 });
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<RegionViewModelResponse>>>(okResult.Value);
        Assert.Equal("Regiões recuperadas com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithRegion()
    {
        var regionResponse = new RegionViewModelResponse { Id = 1, Name = "Region 1" };
        _mockRegionService.Setup(service => service.GetByIdAsync(1))
            .ReturnsAsync(regionResponse);

        var result = await _controller.GetById(1);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<RegionViewModelResponse>>(okResult.Value);
        Assert.Equal("Região recuperada com sucesso.", response.Message);
        Assert.Equal(regionResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenRegionNotFound()
    {
        _mockRegionService.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Região não encontrada."));
        
        var result = await _controller.GetById(1);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<RegionViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Região não encontrada.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithRegion()
    {
        var regionViewModel = new RegionViewModel { Name = "New Region", StateId = 1 };
        var regionResponse = new RegionViewModelResponse { Id = 1, Name = "New Region", StateId = 1 };
        _mockRegionService.Setup(service => service.CreateAsync(regionViewModel))
            .ReturnsAsync(regionResponse);
        
        var result = await _controller.Create(regionViewModel);

        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<RegionViewModelResponse>>(createdResult.Value);
        Assert.Equal("Região registrada com sucesso.", response.Message);
        Assert.Equal(regionResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedRegion()
    {
        var regionViewModelUpdate = new RegionViewModelUpdate { Name = "Updated Region", StateId = 1 };
        var regionResponse = new RegionViewModelResponse { Id = 1, Name = "Updated Region", StateId = 1 };
        _mockRegionService.Setup(service => service.UpdateAsync(1, regionViewModelUpdate))
            .ReturnsAsync(regionResponse);
        
        var result = await _controller.Update(1, regionViewModelUpdate);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<RegionViewModelResponse>>(okResult.Value);
        Assert.Equal("Região atualizada com sucesso.", response.Message);
        Assert.Equal(regionResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundResult_WhenRegionNotFound()
    {
        var regionViewModelUpdate = new RegionViewModelUpdate { Name = "Updated Region", StateId = 1 };
        _mockRegionService.Setup(service => service.UpdateAsync(1, regionViewModelUpdate))
            .ThrowsAsync(new NotFoundException("Região não encontrada."));
        
        var result = await _controller.Update(1, regionViewModelUpdate);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<RegionViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Região não encontrada.", response.Message);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        _mockRegionService.Setup(service => service.DeleteAsync(1))
            .Returns(Task.CompletedTask);
        
        var result = await _controller.Delete(1);
        
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundResult_WhenRegionNotFound()
    {
        _mockRegionService.Setup(service => service.DeleteAsync(1))
            .ThrowsAsync(new NotFoundException("Região não encontrada."));
        
        var result = await _controller.Delete(1);
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<RegionViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Região não encontrada.", response.Message);
    }
}