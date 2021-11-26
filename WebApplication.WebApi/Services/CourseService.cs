using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.Data.DbContext;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Courses;

namespace WebApplication.WebApi.Services
{
    public interface ICourseService
    {
        Task<CourseVm> CreateAsync(CreateCourseDto dto);

        Task<List<CourseVm>> GetListAsync();

        Task<bool> DeleteAsync(Guid Id);
    }

    public class CourseService : ICourseService
    {
        private readonly ManagementDbContext _managementDbContext;
        private readonly IMapper _mapper;

        public CourseService(ManagementDbContext managementDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _managementDbContext = managementDbContext;
        }

        public async Task<CourseVm> CreateAsync(CreateCourseDto dto)
        {
            var course = _mapper.Map<CreateCourseDto, Course>(dto);
            course.CreateTime = DateTime.Now;
            var result = await _managementDbContext.Courses.AddAsync(course);
            await _managementDbContext.SaveChangesAsync();
            return _mapper.Map<Course, CourseVm>(result.Entity);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var course = await _managementDbContext.Courses.FindAsync(Id);
            if (course == null) return false;
            _managementDbContext.Remove(course);
            await _managementDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CourseVm>> GetListAsync()
        {
            return _mapper.Map<List<Course>, List<CourseVm>>(await _managementDbContext.Courses.ToListAsync());
        }
    }
}