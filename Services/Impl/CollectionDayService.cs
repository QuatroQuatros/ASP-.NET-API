using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services.Impl;

public class CollectionDayService : CrudService<CollectionDayModel, CollectionDayViewModel, CollectionDayViewModelResponse, CollectionDayViewModelUpdate>, ICollectionDayService
{
     private readonly ICollectionDayRepository _repository;
     private readonly IStreetRepository _streetRepository;
     private readonly IGarbageCollectionTypeRepository _garbageCollectionTypeRepository;
    

    public CollectionDayService(ICollectionDayRepository repository, IStreetRepository streetRepository, IGarbageCollectionTypeRepository garbageCollectionTypeRepository): base(repository)
    {
        _repository = repository;
        _streetRepository = streetRepository;
        _garbageCollectionTypeRepository = garbageCollectionTypeRepository;
    }

    public override async Task<CollectionDayViewModelResponse> CreateAsync(CollectionDayViewModel viewModel)
    {
        await CheckIfStreetExists(viewModel.StreetId);
        await CheckIfCollectionTypeExists(viewModel.GarbageCollectionTypeId);

        var collectionDay = new CollectionDayModel
        {
            StreetId = viewModel.StreetId,
            GarbageCollectionTypeId = viewModel.GarbageCollectionTypeId,
            ScheduleDate = viewModel.ScheduleDate ?? DateTime.Now,
            Status = viewModel.Status.HasValue ? (CollectionStatus)viewModel.Status.Value : CollectionStatus.Agendado
        };

        await _repository.CreateAsync(collectionDay);
        return MapToViewModelResponse(collectionDay);
    }

    public override async Task<CollectionDayViewModelResponse> UpdateAsync(int id, CollectionDayViewModelUpdate viewModelUpdate)
    {
        await CheckIfStreetExists(viewModelUpdate.StreetId);
        await CheckIfCollectionTypeExists(viewModelUpdate.GarbageCollectionTypeId);

        try
        {
            var collectionDay = await _repository.GetByIdAsync(id);
            if (collectionDay == null)
            {
                throw new NotFoundException("Agendamento não encontrado.");
            }

            if (viewModelUpdate.CollectionDate < viewModelUpdate.ScheduleDate)
            {
                throw new ConflictException("A data de coleta não pode ser menor que a data de agendamento.");
            }

            collectionDay.StreetId = viewModelUpdate.StreetId;
            collectionDay.GarbageCollectionTypeId = viewModelUpdate.GarbageCollectionTypeId;
            collectionDay.ScheduleDate = viewModelUpdate.ScheduleDate;
            collectionDay.CollectionDate = viewModelUpdate.CollectionDate;
            collectionDay.Status = (CollectionStatus)viewModelUpdate.Status;

            await _repository.UpdateAsync(collectionDay);
            return MapToViewModelResponse(collectionDay);
        }catch (NotFoundException e)
        {
            throw new NotFoundException("Agendamento não encontrado.");
        }
        
    }


    protected override CollectionDayModel MapToEntity(CollectionDayViewModel viewModel)
    {
        return new CollectionDayModel
        {
            StreetId = viewModel.StreetId,
            GarbageCollectionTypeId = viewModel.GarbageCollectionTypeId,
            ScheduleDate = viewModel.ScheduleDate ?? DateTime.Now,
            CollectionDate = viewModel.CollectionDate,
            Status = viewModel.Status.HasValue ? (CollectionStatus)viewModel.Status.Value : CollectionStatus.Agendado
        };
    }

    protected override CollectionDayViewModelResponse MapToViewModelResponse(CollectionDayModel entity)
    {
        return new CollectionDayViewModelResponse
        {
            Id = entity.Id,
            StreetId = entity.StreetId,
            GarbageCollectionTypeId = entity.GarbageCollectionTypeId,
            ScheduleDate = entity.ScheduleDate,
            CollectionDate = entity.CollectionDate,
            Status = entity.Status.ToString(),
            StreetName = entity.Street?.Name ?? string.Empty,
            GarbageCollectionTypeName = entity.GarbageCollectionType?.Name ?? string.Empty
        };
    }

    protected override void UpdateEntity(CollectionDayModel entity, CollectionDayViewModelUpdate viewModelUpdate)
    {
        entity.StreetId = viewModelUpdate.StreetId;
        entity.GarbageCollectionTypeId = viewModelUpdate.GarbageCollectionTypeId;
        entity.ScheduleDate = viewModelUpdate.ScheduleDate;
        entity.CollectionDate = viewModelUpdate.CollectionDate;
        entity.Status = (CollectionStatus)viewModelUpdate.Status;
    }
    
    private async Task CheckIfStreetExists(int streetId)
    {
        try
        {
            await _streetRepository.GetByIdAsync(streetId);

        }catch (NotFoundException e)
        {
            throw new NotFoundException("Rua não encontrada.");
        }
    }
    
    private async Task CheckIfCollectionTypeExists(int collectionTypeId)
    {
        try
        {
            await _garbageCollectionTypeRepository.GetByIdAsync(collectionTypeId);

        }catch (NotFoundException e)
        {
            throw new NotFoundException("Tipo de coleta não encontrado.");
        }
    }
}