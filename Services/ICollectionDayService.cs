using GestaoDeResiduos.Models;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services;

public interface ICollectionDayService : ICrudService<CollectionDayModel, CollectionDayViewModel, CollectionDayViewModelResponse, CollectionDayViewModelUpdate>
{
    Task<CollectionDayViewModelResponse> MarkAsCompleteAsync(int id);
    Task<CollectionDayViewModelResponse> MarkAsCanceledAsync(int id);
    Task<CollectionDayViewModelResponse> MarkAsInProgressAsync(int id);
}