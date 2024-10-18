using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services.Impl;

public class StreetService : CrudService<StreetModel, StreetViewModel, StreetViewModelResponse, StreetViewModelUpdate>, IStreetService
{
    
    private readonly IStreetRepository _repository;
    private readonly IDistrictRepository _districtRepository;

    public StreetService(IStreetRepository repository, IDistrictRepository districtRepository): base(repository)
    {
        _repository = repository;
        _districtRepository = districtRepository;
    }

    public override async Task<StreetViewModelResponse> CreateAsync(StreetViewModel viewModel)
    {

        await CheckIfDistrictExists(viewModel.DistrictId);
        
        var district = new StreetModel
        {
            Name = viewModel.Name,
            DistrictId = viewModel.DistrictId
        };
        
        await _repository.CreateAsync(district);
        return MapToViewModelResponse(district);
    }
    
    public override async Task<StreetViewModelResponse> UpdateAsync(int id, StreetViewModelUpdate viewModelUpdate)
    {
        await CheckIfDistrictExists(viewModelUpdate.DistrictId);
        try
        {
            var street = await _repository.GetByIdAsync(id);
            street.Name = viewModelUpdate.Name;
            street.DistrictId = viewModelUpdate.DistrictId;

            await _repository.UpdateAsync(street);
            return MapToViewModelResponse(street);
        }catch (NotFoundException)
        {
            throw new NotFoundException("Rua não encontrada.");
        }
    }


    protected override StreetModel MapToEntity(StreetViewModel viewModel)
    {
        return new StreetModel
        {
            Name = viewModel.Name,
            DistrictId = viewModel.DistrictId
        };
    }

    protected override StreetViewModelResponse MapToViewModelResponse(StreetModel entity)
    {
        return new StreetViewModelResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            DistrictId = entity.DistrictId
        };
    }

    protected override void UpdateEntity(StreetModel entity, StreetViewModelUpdate viewModelUpdate)
    {
        entity.Name = viewModelUpdate.Name;
        entity.DistrictId = viewModelUpdate.DistrictId;
    }
    
    private async Task CheckIfDistrictExists(int districtId)
    {
        try
        {
            await _districtRepository.GetByIdAsync(districtId);

        }catch (NotFoundException)
        {
            throw new NotFoundException("Bairro não encontrado.");
        }

    }
}