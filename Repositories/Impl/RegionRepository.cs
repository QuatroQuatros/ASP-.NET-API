using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;

namespace GestaoDeResiduos.Repositories.Impl;

public class RegionRepository : Repository<RegionModel>, IRegionRepository
{
    
    public RegionRepository(DatabaseContext context) : base(context)
    {
    }
    
}