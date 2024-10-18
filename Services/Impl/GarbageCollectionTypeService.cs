using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services.Impl;

public class GarbageCollectionTypeService : CrudService<GarbageCollectionTypeModel, GarbageCollectionTypeViewModel, GarbageCollectionTypeViewModelResponse, GarbageCollectionTypeViewModelUpdate>, IGarbageCollectionTypeService
{
    private readonly IGarbageCollectionTypeRepository _repository;

    public GarbageCollectionTypeService(IGarbageCollectionTypeRepository repository) : base(repository)
    {
        _repository = repository;
    }
    
    protected override GarbageCollectionTypeModel MapToEntity(GarbageCollectionTypeViewModel viewModel)
    {
        return new GarbageCollectionTypeModel
        {
            Name = viewModel.Name
        };
    }

    protected override GarbageCollectionTypeViewModelResponse MapToViewModelResponse(GarbageCollectionTypeModel entity)
    {
        return new GarbageCollectionTypeViewModelResponse
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    protected override void UpdateEntity(GarbageCollectionTypeModel entity, GarbageCollectionTypeViewModelUpdate viewModelUpdate)
    {
        entity.Name = viewModelUpdate.Name;
    }
    
}