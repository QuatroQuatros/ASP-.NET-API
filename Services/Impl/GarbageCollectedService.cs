using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services.Impl;

public class GarbageCollectedService : CrudService<GarbageCollectedModel, GarbageCollectedViewModel, GarbageCollectedViewModelResponse, GarbageCollectedViewModelUpdate>, IGarbageCollectedService
{
        private readonly IGarbageCollectedRepository _garbageCollectedRepository;
        private readonly ICollectionDayRepository _collectionDayRepository;


        public GarbageCollectedService(IGarbageCollectedRepository repository, ICollectionDayRepository collectionDayRepository) : base(repository)
        {
            _garbageCollectedRepository  = repository;
            _collectionDayRepository = collectionDayRepository;
        }

        public override async Task<GarbageCollectedViewModelResponse> CreateAsync(GarbageCollectedViewModel viewModel)
        {

            await CheckIfCollectionDayExists(viewModel.CollectionDayId);
            
            var entity = MapToEntity(viewModel);
            await _garbageCollectedRepository.CreateAsync(entity);
            return MapToViewModelResponse(entity);

        }

        protected override GarbageCollectedModel MapToEntity(GarbageCollectedViewModel viewModel)
        {
            return new GarbageCollectedModel
            {
                CollectionDayId = viewModel.CollectionDayId,
                Amount = viewModel.Amount
            };
        }

        protected override GarbageCollectedViewModelResponse MapToViewModelResponse(GarbageCollectedModel entity)
        {
            return new GarbageCollectedViewModelResponse
            {
                Id = entity.Id,
                CollectionDayId = entity.CollectionDayId,
                Amount = entity.Amount
            };
        }

        protected override void UpdateEntity(GarbageCollectedModel entity, GarbageCollectedViewModelUpdate viewModelUpdate)
        {
            entity.Amount = viewModelUpdate.Amount;
        }
        
        public async Task<TrashResultState> GetStateMoreTrashAsync(int? stateId = null, int? collectionTypeId = null)
        {
            return await _garbageCollectedRepository.GetStateMoreTrashAsync(stateId, collectionTypeId);
        }
        
        public async Task<TrashResultRegion> GetRegionMoreTrashAsync(int? regionId = null, int? collectionTypeId = null)
        {
            return await _garbageCollectedRepository.GetRegionMoreTrashAsync(regionId, collectionTypeId);
        }
        
        public async Task<TrashResultNeighborhood> GetNeighborhoodMoreTrashAsync(int? districtId = null, int? collectionTypeId = null)
        {
            return await _garbageCollectedRepository.GetNeighborhoodMoreTrashAsync(districtId, collectionTypeId);
        }
        
        private async Task CheckIfCollectionDayExists(int collectionDayId)
        {
            try
            {
                await _collectionDayRepository.GetByIdAsync(collectionDayId);

            }catch (NotFoundException)
            {
                throw new NotFoundException("Agendamento não encontrado.");
            }

        }
        
        
        
}