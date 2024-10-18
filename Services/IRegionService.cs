using GestaoDeResiduos.Models;
using GestaoDeResiduos.ViewModels;
using GestaoDeResiduos.ViewModels.Responses;
using GestaoDeResiduos.ViewModels.Update;

namespace GestaoDeResiduos.Services;

public interface IRegionService : ICrudService<RegionModel, RegionViewModel, RegionViewModelResponse, RegionViewModelUpdate>
{

}