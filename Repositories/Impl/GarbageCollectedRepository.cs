using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;

namespace GestaoDeResiduos.Repositories.Impl;

public class GarbageCollectedRepository : Repository<GarbageCollectedModel>, IGarbageCollectedRepository
{
    public GarbageCollectedRepository(DatabaseContext context) : base(context)
    {
    }
}