using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services.Impl;

public class RegionService : CrudService<RegionModel, RegionViewModel, RegionViewModelResponse, RegionViewModelUpdate>, IRegionService
{
    private readonly IRegionRepository _repository;
    private readonly IStateRepository _stateRepository;

    public RegionService(IRegionRepository repository, IStateRepository stateRepository) : base(repository)
    {
        _repository = repository;
        _stateRepository = stateRepository;
    }

    public override async Task<RegionViewModelResponse> CreateAsync(RegionViewModel regionViewModel)
    {

        await CheckIfStateExists(regionViewModel.StateId);
        try
        {
            var region = new RegionModel
            {
                Name = regionViewModel.Name,
                StateId = regionViewModel.StateId
            };
        
            await _repository.CreateAsync(region);
            return MapToViewModelResponse(region);
        }catch (ConflictException e)
        {
            throw new ConflictException("Estado não encontrado.");
        }
        
    }
    
    public override async Task<RegionViewModelResponse> UpdateAsync(int id, RegionViewModelUpdate viewModelUpdate)
    {
        await CheckIfStateExists(viewModelUpdate.StateId);

        try
        {
            var region = await _repository.GetByIdAsync(id);
            region.Name = viewModelUpdate.Name;
            region.StateId = viewModelUpdate.StateId;

            await _repository.UpdateAsync(region);
            return MapToViewModelResponse(region);
        }catch (NotFoundException e)
        {
            throw new NotFoundException("Região não encontrada.");
        }
        

       
    }


    protected override RegionModel MapToEntity(RegionViewModel viewModel)
    {
        return new RegionModel
        {
            Name = viewModel.Name,
            StateId = viewModel.StateId
        };
    }

    protected override RegionViewModelResponse MapToViewModelResponse(RegionModel entity)
    {
        return new RegionViewModelResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            StateId = entity.StateId
        };
    }

    protected override void UpdateEntity(RegionModel entity, RegionViewModelUpdate viewModelUpdate)
    {
        entity.Name = viewModelUpdate.Name;
        entity.StateId = viewModelUpdate.StateId;
    }
    
    private async Task CheckIfStateExists(int stateId)
    {
        try
        {
            await _stateRepository.GetByIdAsync(stateId);

        }catch (NotFoundException e)
        {
            throw new NotFoundException("Estado não encontrado.");
        }

    }
}