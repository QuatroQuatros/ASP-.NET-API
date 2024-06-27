using GestaoDeResiduos.Models;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services
{
    public interface IStateService : ICrudService<StateModel, StateViewModel, StateViewModelResponse, StateViewModelUpdate>
    {
        Task<StateViewModelResponse> GetByUfAsync(string uf);
    }
}