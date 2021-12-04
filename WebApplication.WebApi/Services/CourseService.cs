using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.WebApi.Data.DbContext;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Common;
using WebApplication.WebApi.ViewModels.Courses;
using WebApplication.WebApi.ViewModels.Users;

namespace WebApplication.WebApi.Services
{
    public interface ICourseService
    {
        Task<CourseVm> CreateAsync(CreateCourseDto dto);

        Task<PagedResultDto<CourseVm>> GetListAsync(PagedAndSortedResultRequestDto request);

        Task<bool> DeleteAsync(Guid Id);

        Task<CourseVm> UpdateAsync(UpdateCourseDto dto);

        Task<CourseVm> GetById(Guid Id);

        Task<PagedResultDto<CourseVm>> GetAllListAsync(PagedAndSortedResultRequestDto request);
    }

    public class CourseService : ICourseService
    {
        private readonly ManagementDbContext _managementDbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStorageService _storageService;
        private Guid UserId;

        public CourseService(IHttpContextAccessor httpContextAccessor, ManagementDbContext managementDbContext, IMapper mapper, UserManager<AppUser> userManager, IStorageService storageService)
        {
            _storageService = storageService;
            _userManager = userManager;
            _mapper = mapper;
            _managementDbContext = managementDbContext;
            UserId = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated? new Guid(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value):Guid.Empty;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<CourseVm> CreateAsync(CreateCourseDto dto)
        {
            var course = _mapper.Map<CreateCourseDto, Course>(dto);
            course.Image = await SaveFile(dto.Image);
            course.CreateTime = DateTime.Now;
            course.CreatorId = UserId;
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

        public async Task<CourseVm> GetById(Guid Id)
        {
            var query = from c in _managementDbContext.Courses
                        join uc in _managementDbContext.UserCourses on c.Id equals uc.CourseId
                        join u in _userManager.Users on uc.UserId equals u.Id
                        where c.Id.Equals(Id)
                        select new { c, uc, u };
            var data = await query.Select(x => new CourseVm()
            {
                User = _mapper.Map<List<UserVm>>(x.u.UserCourses.Select(x => x.AppUser)),
                Name = x.c.Name,
                Description = x.c.Description,
                Id = x.c.Id,
                Start_Date = x.c.Start_Date,
                End_Date = x.c.End_Date
            }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<PagedResultDto<CourseVm>> GetListAsync(PagedAndSortedResultRequestDto request)
        {
            var query = _managementDbContext.Courses.Include(x => x.UserCourses).ThenInclude(x => x.AppUser);
            var totalItems = query.Count();
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query.Where(x => x.Name.Contains(request.Filter));
            }
            if (string.IsNullOrEmpty(request.Sorting)) request.Sorting = nameof(Topic.Name);
            var tm = await query.OrderBy(x => x.Id).Skip((request.SkipCount - 1) * request.MaxResultCount).Take(request.MaxResultCount)
                .ToListAsync();
            var data = tm.Select(x => new CourseVm()
            {
                User = _mapper.Map<List<UserVm>>(x.UserCourses.Select(x => x.AppUser)),
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                Start_Date = x.Start_Date,
                End_Date = x.End_Date,
                Image = x.Image
            }).ToList();

            return new PagedResultDto<CourseVm>
            {
                Items = data,
                totalCount = totalItems
            };
        }

        public async Task<CourseVm> UpdateAsync(UpdateCourseDto dto)
        {
            var course = await _managementDbContext.Courses.FindAsync(dto.Id);
            if (course != null) return null;
            course.Name = dto.Name;
            course.UpdateTime = DateTime.Now;
            course.Description = dto.Description;
            course.UpdaterId = UserId;
            await _managementDbContext.SaveChangesAsync();
            return _mapper.Map<CourseVm>(course);
        }

        public async Task<PagedResultDto<CourseVm>> GetAllListAsync(PagedAndSortedResultRequestDto request)
        {
            var query = _managementDbContext.Courses;
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query.Where(x => x.Name.Contains(request.Filter));
            }
            if (string.IsNullOrEmpty(request.Sorting)) request.Sorting = nameof(Course.Name);
            var tm = await query.OrderBy(x => x.Id).Skip((request.SkipCount - 1) * request.MaxResultCount).Take(request.MaxResultCount)
                .ToListAsync();

            return new PagedResultDto<CourseVm>
            {
                Items = _mapper.Map<List<CourseVm>>(tm),
                totalCount = query.Count()
            };
        }
    }
}