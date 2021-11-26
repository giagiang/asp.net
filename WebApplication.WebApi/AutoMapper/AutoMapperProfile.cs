using AutoMapper;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Courses;
using WebApplication.WebApi.ViewModels.Topics;
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
            CreateMap<Topic, TopicVm>().ReverseMap();
            CreateMap<Topic, TopicCreateDto>().ReverseMap();
            CreateMap<Course, CourseVm>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
        }
    }
}