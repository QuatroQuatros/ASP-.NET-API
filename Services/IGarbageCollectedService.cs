using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories.Impl;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services;

public interface IGarbageCollectedService : ICrudService<GarbageCollectedModel, GarbageCollectedViewModel, GarbageCollectedViewModelResponse, GarbageCollectedViewModelUpdate>
{
     Task<TrashResultState> GetStateMoreTrashAsync(int? stateId = null, int? collectionTypeId = null);
     
     Task<TrashResultRegion> GetRegionMoreTrashAsync(int? regionId = null, int? collectionTypeId = null);
     
     Task<TrashResultNeighborhood> GetNeighborhoodMoreTrashAsync(int? districtId = null, int? collectionTypeId = null);
}