using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;

namespace GestaoDeResiduos.Repositories.Impl;

public class CollectionDayRepository : Repository<CollectionDayModel>, ICollectionDayRepository
{
    public CollectionDayRepository(DatabaseContext context) : base(context)
    {
    }
    
}