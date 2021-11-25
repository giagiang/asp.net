using AutoMapper;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Users;

namespace WebApplication.WebApi.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, UserVm>().ReverseMap();
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
        }
    }
}