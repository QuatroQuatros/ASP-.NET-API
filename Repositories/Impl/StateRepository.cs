using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeResiduos.Repositories.Impl;

public class StateRepository : Repository<StateModel>, IStateRepository
{
    public StateRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<StateModel> GetStateByUFAsync(string uf)
    {
        return await _context.States.FirstOrDefaultAsync(state => state.UF == uf);
    }
}