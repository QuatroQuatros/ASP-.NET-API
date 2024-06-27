using AutoMapper;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Responses;
using GestaoDeResiduos.ViewModels;


namespace GestaoDeResiduos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, UserModel>();
            CreateMap<UserViewModel, LoginViewModel>();
            CreateMap<UserViewModel, UserViewModelResponse>();
            CreateMap<LoginViewModel, UserViewModel>();
            CreateMap<UserModel, UserViewModel>();
            CreateMap<UserModel, UserViewModelResponse>();
        }
    }
}
