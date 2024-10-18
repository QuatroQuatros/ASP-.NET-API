using GestaoDeResiduos.Models;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services
{
    public interface IStateService : ICrudService<StateModel, StateViewModel, StateViewModelResponse, StateViewModelUpdate>
    {
        Task<StateViewModelResponse> GetByUfAsync(string uf);
    }
}