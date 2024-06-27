using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;

namespace GestaoDeResiduos.Repositories.Impl;

public class StreetRepository : Repository<StreetModel>, IStreetRepository
{
    public StreetRepository(DatabaseContext context) : base(context)
    {
    }
}