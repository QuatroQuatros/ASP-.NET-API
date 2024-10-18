using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories.Impl;
using GestaoDeResiduos.ViewModels.Responses;

namespace GestaoDeResiduos.Repositories;

public interface IGarbageCollectedRepository : IRepository<GarbageCollectedModel>
{
    Task<TrashResultState> GetStateMoreTrashAsync(int? stateId = null, int? collectionTypeId = null);
    Task<TrashResultRegion> GetRegionMoreTrashAsync(int? regionId = null, int? collectionTypeId = null);
    Task<TrashResultNeighborhood> GetNeighborhoodMoreTrashAsync(int? regionId = null, int? collectionTypeId = null);
}