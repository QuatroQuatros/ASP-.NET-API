﻿using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services.Impl;

public class DistrictService : CrudService<DistrictModel, DistrictViewModel, DistrictViewModelResponse, DistrictViewModelUpdate>, IDistrictService
{
    private readonly IDistrictRepository _repository;
    private readonly IRegionRepository _regionrepository;

    public DistrictService(IDistrictRepository repository, IRegionRepository regionrepository): base(repository)
    {
        _repository = repository;
        _regionrepository = regionrepository;
    }

    public override async Task<DistrictViewModelResponse> CreateAsync(DistrictViewModel districtViewModel)
    {

        await CheckIfRegionExists(districtViewModel.RegionId);
        
        var district = new DistrictModel
        {
            Name = districtViewModel.Name,
            RegionId = districtViewModel.RegionId
        };
        
        await _repository.CreateAsync(district);
        return MapToViewModelResponse(district);
    }
    
    public override async Task<DistrictViewModelResponse> UpdateAsync(int id, DistrictViewModelUpdate viewModelUpdate)
    {
        await CheckIfRegionExists(viewModelUpdate.RegionId);

        try
        {
            var district = await _repository.GetByIdAsync(id);
            district.Name = viewModelUpdate.Name;
            district.RegionId = viewModelUpdate.RegionId;

            await _repository.UpdateAsync(district);
            return MapToViewModelResponse(district);
        }catch (NotFoundException)
        {
            throw new NotFoundException("Bairro não encontrado.");
        }
    }


    protected override DistrictModel MapToEntity(DistrictViewModel viewModel)
    {
        return new DistrictModel
        {
            Name = viewModel.Name,
            RegionId = viewModel.RegionId
        };
    }

    protected override DistrictViewModelResponse MapToViewModelResponse(DistrictModel entity)
    {
        return new DistrictViewModelResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            RegionId = entity.RegionId
        };
    }

    protected override void UpdateEntity(DistrictModel entity, DistrictViewModelUpdate viewModelUpdate)
    {
        entity.Name = viewModelUpdate.Name;
        entity.RegionId = viewModelUpdate.RegionId;
    }
    
    private async Task CheckIfRegionExists(int regionId)
    {
        try
        {
            await _regionrepository.GetByIdAsync(regionId);

        }catch (NotFoundException)
        {
            throw new NotFoundException("Região não encontrada.");
        }

    }
}