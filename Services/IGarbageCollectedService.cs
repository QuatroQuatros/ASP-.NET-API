using GestaoDeResiduos.Models;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services;

public interface IGarbageCollectedService : ICrudService<GarbageCollectedModel, GarbageCollectedViewModel, GarbageCollectedViewModelResponse, GarbageCollectedViewModelUpdate>
{
     Task<TrashResultState> GetStateMoreTrashAsync(int? stateId = null, int? collectionTypeId = null);
     
     Task<TrashResultRegion> GetRegionMoreTrashAsync(int? regionId = null, int? collectionTypeId = null);
     
     Task<TrashResultNeighborhood> GetNeighborhoodMoreTrashAsync(int? districtId = null, int? collectionTypeId = null);
}