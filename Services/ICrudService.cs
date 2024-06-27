using System.Threading.Tasks;
using GestaoDeResiduos.Responses;

namespace GestaoDeResiduos.Services
{
    public interface ICrudService<TEntity, TViewModel, TViewModelResponse, TViewModelUpdate>
        where TEntity : class
    {
        Task<PaginatedResponse<TViewModelResponse>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<TViewModelResponse> GetByIdAsync(int id);
        Task<TViewModelResponse> CreateAsync(TViewModel viewModel);
        Task<TViewModelResponse> UpdateAsync(int id, TViewModelUpdate viewModelUpdate);
        Task DeleteAsync(int id);
    }
}