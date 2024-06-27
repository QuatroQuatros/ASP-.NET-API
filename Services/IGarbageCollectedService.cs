using GestaoDeResiduos.Models;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services;

public interface IGarbageCollectedService : ICrudService<GarbageCollectedModel, GarbageCollectedViewModel, GarbageCollectedViewModelResponse, GarbageCollectedViewModelUpdate>
{
    
}