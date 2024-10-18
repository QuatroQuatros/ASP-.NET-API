using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Services;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoDeResiduos.Test;

public class DistrictControllerTests
{
    private readonly Mock<IDistrictService> _mockDistrictService;
    private readonly DistrictController _controller;

    public DistrictControllerTests()
    {
        _mockDistrictService = new Mock<IDistrictService>();
        _controller = new DistrictController(_mockDistrictService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithPaginatedDistricts()
    {
        var paginatedResponse = new PaginatedResponse<DistrictViewModelResponse>
        {
            Items = new List<DistrictViewModelResponse>(),
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 0
        };
        _mockDistrictService.Setup(service => service.GetPaginatedAsync(1, 10))
            .ReturnsAsync(paginatedResponse);

        var result = await _controller.GetAll(new Pagination { Page = 1, Size = 10 });

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<PaginatedResponse<DistrictViewModelResponse>>>(okResult.Value);
        Assert.Equal("Bairros recuperados com sucesso.", response.Message);
        Assert.Equal(paginatedResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithDistrict()
    {
        var districtResponse = new DistrictViewModelResponse { Id = 1, Name = "District 1" };
        _mockDistrictService.Setup(service => service.GetByIdAsync(1))
            .ReturnsAsync(districtResponse);

        var result = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<DistrictViewModelResponse>>(okResult.Value);
        Assert.Equal("Bairro recuperado com sucesso.", response.Message);
        Assert.Equal(districtResponse, response.Data);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundResult_WhenDistrictNotFound()
    {
        _mockDistrictService.Setup(service => service.GetByIdAsync(1))
            .ThrowsAsync(new NotFoundException("Bairro não encontrado."));

        var result = await _controller.GetById(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<DistrictViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Bairro não encontrado.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithDistrict()
    {
        var districtViewModel = new DistrictViewModel { Name = "New District", RegionId = 1 };
        var districtResponse = new DistrictViewModelResponse { Id = 1, Name = "New District", RegionId = 1 };
        _mockDistrictService.Setup(service => service.CreateAsync(districtViewModel))
            .ReturnsAsync(districtResponse);

        var result = await _controller.Create(districtViewModel);

        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<DistrictViewModelResponse>>(createdResult.Value);
        Assert.Equal("Bairro registrada com sucesso.", response.Message);
        Assert.Equal(districtResponse, response.Data);
        Assert.Equal($"/api/districts/{districtResponse.Id}", createdResult.Location);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedDistrict()
    {
        var districtViewModelUpdate = new DistrictViewModelUpdate { Name = "Updated District", RegionId = 1 };
        var districtResponse = new DistrictViewModelResponse { Id = 1, Name = "Updated District", RegionId = 1 };
        _mockDistrictService.Setup(service => service.UpdateAsync(1, districtViewModelUpdate))
            .ReturnsAsync(districtResponse);

        var result = await _controller.Update(1, districtViewModelUpdate);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<DistrictViewModelResponse>>(okResult.Value);
        Assert.Equal("Bairro atualizado com sucesso.", response.Message);
        Assert.Equal(districtResponse, response.Data);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundResult_WhenDistrictNotFound()
    {
        var districtViewModelUpdate = new DistrictViewModelUpdate { Name = "Updated District", RegionId = 1 };
        _mockDistrictService.Setup(service => service.UpdateAsync(1, districtViewModelUpdate))
            .ThrowsAsync(new NotFoundException("Bairro não encontrado."));

        var result = await _controller.Update(1, districtViewModelUpdate);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<DistrictViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Bairro não encontrado.", response.Message);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        _mockDistrictService.Setup(service => service.DeleteAsync(1))
            .Returns(Task.CompletedTask);

        var result = await _controller.Delete(1);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundResult_WhenDistrictNotFound()
    {
        _mockDistrictService.Setup(service => service.DeleteAsync(1))
            .ThrowsAsync(new NotFoundException("Bairro não encontrado."));

        var result = await _controller.Delete(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        var response = Assert.IsType<BaseApiResponse<DistrictViewModelResponse>>(notFoundResult.Value);
        Assert.Equal("Bairro não encontrado.", response.Message);
    }
}