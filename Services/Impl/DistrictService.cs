using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;
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
        var region = await _regionrepository.GetByIdAsync(districtViewModel.RegionId);
        if (region == null)
        {
            throw new ConflictException("Região não encontrada.");
        }
        
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
        try
        {
            var region = await _regionrepository.GetByIdAsync(viewModelUpdate.RegionId);
            if (region == null)
            {
                throw new ConflictException("Região não encontrada.");
            }
        }catch (NotFoundException e)
        {
            throw new ConflictException("Região não encontrada.");
        }

        try
        {
            var district = await _repository.GetByIdAsync(id);
            if (district == null)
            {
                throw new NotFoundException("Bairro não encontrado.");
            }
            district.Name = viewModelUpdate.Name;
            district.RegionId = viewModelUpdate.RegionId;

            await _repository.UpdateAsync(district);
            return MapToViewModelResponse(district);
        }catch (NotFoundException e)
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
}