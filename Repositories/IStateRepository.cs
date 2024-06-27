using GestaoDeResiduos.Models;

namespace GestaoDeResiduos.Repositories;

public interface IStateRepository : IRepository<StateModel>
{
    Task<StateModel> GetStateByUFAsync(string uf);
}