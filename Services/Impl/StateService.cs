using System.Threading.Tasks;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Repositories;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Update;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeResiduos.Services.Impl
{
    public class StateService : CrudService<StateModel, StateViewModel, StateViewModelResponse, StateViewModelUpdate>, IStateService
    {
        private readonly IStateRepository _stateRepository;


        public StateService(IStateRepository repository) : base(repository)
        {
            _stateRepository  = repository;
        }

        public override async Task<StateViewModelResponse> CreateAsync(StateViewModel viewModel)
        {
            if (!BrazilianStates.ValidUFs.Contains(viewModel.UF))
            {
                throw new ConflictException("UF inválida.");
            }

            var exists = await _stateRepository.GetStateByUFAsync(viewModel.UF);
            if (exists != null)
            {
                throw new ConflictException("UF já existe.");
            }
            
            var entity = MapToEntity(viewModel);
            await _stateRepository.CreateAsync(entity);
            return MapToViewModelResponse(entity);

        }

        protected override StateModel MapToEntity(StateViewModel viewModel)
        {
            return new StateModel
            {
                Name = viewModel.Name,
                UF = viewModel.UF
            };
        }

        protected override StateViewModelResponse MapToViewModelResponse(StateModel entity)
        {
            return new StateViewModelResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                UF = entity.UF
            };
        }

        protected override void UpdateEntity(StateModel entity, StateViewModelUpdate viewModelUpdate)
        {
            entity.Name = viewModelUpdate.Name;
            entity.UF = viewModelUpdate.UF;
        }
        
        public async Task<StateViewModelResponse> GetByUfAsync(string uf)
        {
            if (!BrazilianStates.ValidUFs.Contains(uf))
            {
                throw new ConflictException("UF inválida.");
            }
            
            var entity = await _stateRepository.GetStateByUFAsync(uf);
            if (entity == null)
            {
                throw new NotFoundException("Estado não encontrado.");
            }
            return MapToViewModelResponse(entity);
        }
    }
}