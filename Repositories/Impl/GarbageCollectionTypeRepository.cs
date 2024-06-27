using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;

namespace GestaoDeResiduos.Repositories.Impl;

public class GarbageCollectionTypeRepository : Repository<GarbageCollectionTypeModel>, IGarbageCollectionTypeRepository
{
 
    public GarbageCollectionTypeRepository(DatabaseContext context) : base(context)
    {
    }
}