using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;

namespace GestaoDeResiduos.Repositories.Impl;

public class DistrictRepository : Repository<DistrictModel>, IDistrictRepository
{
    public DistrictRepository(DatabaseContext context) : base(context)
    {
    }
}