using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.Data.DbContext;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Classes;
using WebApplication.WebApi.ViewModels.Common;
using WebApplication.WebApi.ViewModels.Users;

namespace WebApplication.WebApi.Services
{
    public interface IClassService
    {
        Task<ClassVm> CreateAsync(CreateClassDto dto);

        Task<ClassVm> UpdateAsync(UpdateClassDto dto);

        Task<PagedResultDto<ClassVm>> GetListAsync(PagedAndSortedResultRequestDto request);

        Task<bool> DeleteAsync(Guid Id);

        Task<ClassVm> GetById(Guid Id);

        Task<PagedResultDto<ClassVm>> GetAllListAsync(PagedAndSortedResultRequestDto request);
    }

    public class CLassService : IClassService
    {
        private readonly ManagementDbContext _managementDbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CLassService(ManagementDbContext managementDbContext, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _managementDbContext = managementDbContext;
        }

        public async Task<ClassVm> CreateAsync(CreateClassDto dto)
        {
            var t = _mapper.Map<CreateClassDto, Class>(dto);
            t.CreateTime = DateTime.Now;
            var topic = await _managementDbContext.Classes.AddAsync(t);
            await _managementDbContext.SaveChangesAsync();
            if (topic != null)
            {
                return _mapper.Map<Class, ClassVm>(topic.Entity);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var topic = await _managementDbContext.Classes.FindAsync(Id);
            if (topic == null) return false;
            _managementDbContext.Classes.Remove(topic);
            await _managementDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultDto<ClassVm>> GetListAsync(PagedAndSortedResultRequestDto request)
        {
            var query = _managementDbContext.Classes.Include(x => x.UserClasses).ThenInclude(x => x.AppUser);
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query.Where(x => x.Name.Contains(request.Filter));
            }
            if (string.IsNullOrEmpty(request.Sorting)) request.Sorting = nameof(Topic.Name);
            var tm = await query.OrderBy(x => x.Id).Skip((request.SkipCount - 1) * request.MaxResultCount).Take(request.MaxResultCount)
                .ToListAsync();
            var data = tm.Select(x => new ClassVm()
            {
                User = _mapper.Map<UserVm>(x.UserClasses.Select(x => x.AppUser)),
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime,
            }).ToList();

            return new PagedResultDto<ClassVm>
            {
                Items = data,
                totalCount = query.Count()
            };
        }

        public async Task<ClassVm> UpdateAsync(UpdateClassDto dto)
        {
            var topic = await _managementDbContext.Topics.FindAsync(dto.Id);
            if (topic != null) return null;
            topic.Name = dto.Name;
            topic.UpdateTime = DateTime.Now;
            topic.Description = dto.Description;
            await _managementDbContext.SaveChangesAsync();
            return _mapper.Map<ClassVm>(topic);
        }

        public async Task<ClassVm> GetById(Guid Id)
        {
            var query = from c in _managementDbContext.Classes
                        join uc in _managementDbContext.UserClasses on c.Id equals uc.ClassId
                        join u in _userManager.Users on uc.UserId equals u.Id
                        where c.Id.Equals(Id)
                        select new { c, uc, u };
            var data = await query.Select(x => new ClassVm()
            {
                User = _mapper.Map<UserVm>(x.uc.AppUser),
                Name = x.c.Name,
                Description = x.c.Description,
                Id = x.c.Id,
            }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<PagedResultDto<ClassVm>> GetAllListAsync(PagedAndSortedResultRequestDto request)
        {
            var query = _managementDbContext.Classes;
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query.Where(x => x.Name.Contains(request.Filter));
            }
            if (string.IsNullOrEmpty(request.Sorting)) request.Sorting = nameof(Class.Name);
            var tm = await query.OrderBy(x => x.Id).Skip((request.SkipCount - 1) * request.MaxResultCount).Take(request.MaxResultCount)
                .ToListAsync();

            return new PagedResultDto<ClassVm>
            {
                Items = _mapper.Map<List<ClassVm>>(tm),
                totalCount = query.Count()
            };
        }
    }
}