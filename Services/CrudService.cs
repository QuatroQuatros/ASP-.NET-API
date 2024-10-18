using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.ViewModels.Responses;


namespace GestaoDeResiduos.Services
{
    public abstract class CrudService<TEntity, TViewModel, TViewModelResponse, TViewModelUpdate> : ICrudService<TEntity, TViewModel, TViewModelResponse, TViewModelUpdate>
        where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;

        protected CrudService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<PaginatedResponse<TViewModelResponse>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var paginatedEntities = await _repository.GetAllAsync(pageNumber, pageSize);
            var response = new PaginatedResponse<TViewModelResponse>
            {
                Items = paginatedEntities.Items.Select(MapToViewModelResponse).ToList(),
                PageNumber = paginatedEntities.PageNumber,
                PageSize = paginatedEntities.PageSize,
                TotalCount = paginatedEntities.TotalCount
            };
            return response;
        }

        public virtual async Task<TViewModelResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return MapToViewModelResponse(entity);
        }

        public virtual async Task<TViewModelResponse> CreateAsync(TViewModel viewModel)
        {
            var entity = MapToEntity(viewModel);
            await _repository.CreateAsync(entity);
            return MapToViewModelResponse(entity);
        }

        public virtual async Task<TViewModelResponse> UpdateAsync(int id, TViewModelUpdate viewModelUpdate)
        {
            var entity = await _repository.GetByIdAsync(id);
            UpdateEntity(entity, viewModelUpdate);
            await _repository.UpdateAsync(entity);
            return MapToViewModelResponse(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        protected abstract TEntity MapToEntity(TViewModel viewModel);
        protected abstract TViewModelResponse MapToViewModelResponse(TEntity entity);
        protected abstract void UpdateEntity(TEntity entity, TViewModelUpdate viewModelUpdate);
    }
}
