using GestaoDeResiduos.Responses;

namespace GestaoDeResiduos.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<PaginatedResponse<T>> GetAllAsync(int page = 1, int size = 10);
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}