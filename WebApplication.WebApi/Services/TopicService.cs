using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.Data.DbContext;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Common;
using WebApplication.WebApi.ViewModels.Courses;
using WebApplication.WebApi.ViewModels.Topics;

namespace WebApplication.WebApi.Services
{
    public interface ITopicService
    {
        Task<TopicVm> CreateAsync(TopicCreateDto dto);

        Task<TopicVm> UpdateAsync(TopicUpdateDto dto);

        Task<PagedResultDto<TopicVm>> GetListAsync(PagedAndSortedResultRequestDto request);

        Task<bool> DeleteAsync(Guid Id);

        Task<TopicVm> GetById(Guid Id);

        Task<PagedResultDto<TopicVm>> GetAllListAsync(PagedAndSortedResultRequestDto request);
    }

    public class TopicService : ITopicService
    {
        private readonly ManagementDbContext _managementDbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public TopicService(ManagementDbContext managementDbContext, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _managementDbContext = managementDbContext;
        }

        public async Task<TopicVm> CreateAsync(TopicCreateDto dto)
        {
            var t = _mapper.Map<TopicCreateDto, Topic>(dto);
            t.CreateTime = DateTime.Now;
            var topic = await _managementDbContext.Topics.AddAsync(t);
            await _managementDbContext.SaveChangesAsync();
            if (topic != null)
            {
                return _mapper.Map<Topic, TopicVm>(topic.Entity);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var topic = await _managementDbContext.Topics.FindAsync(Id);
            if (topic == null) return false;
            _managementDbContext.Topics.Remove(topic);
            await _managementDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultDto<TopicVm>> GetListAsync(PagedAndSortedResultRequestDto request)
        {
            var query = from t in _managementDbContext.Topics
                        join c in _managementDbContext.Courses on t.CourseId equals c.Id
                        select new TopicVm()
                        {
                            Courses = _mapper.Map<Course, CourseVm>(c),
                            CreateTime = t.CreateTime,
                            Id = t.Id,
                            Description = t.Description,
                            Name = t.Name,
                            UpdateTime = t.UpdateTime,
                            CreatorId = t.CreatorId,
                            DeletorId = t.DeletorId,
                            UpdaterId = t.UpdaterId
                        };
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query = query.Where(x => x.Name.Contains(request.Filter) || x.Courses.Name.Contains(request.Filter));
            }
            if (string.IsNullOrEmpty(request.Sorting)) request.Sorting = nameof(Topic.Name);
            var tm = await query.OrderBy(x => x.Id).Skip((request.SkipCount - 1) * request.MaxResultCount).Take(request.MaxResultCount).ToListAsync();
            var topic = _mapper.Map<List<TopicVm>>(tm); ;
            return new PagedResultDto<TopicVm> { Items = topic, totalCount = tm.Count };
        }

        public async Task<TopicVm> UpdateAsync(TopicUpdateDto dto)
        {
            var topic = await _managementDbContext.Topics.FindAsync(dto.Id);
            if (topic != null) return null;
            topic.Name = dto.Name;
            topic.UpdateTime = DateTime.Now;
            topic.Description = dto.Description;
            if (dto.CourseId != Guid.Empty) topic.CourseId = dto.CourseId;
            await _managementDbContext.SaveChangesAsync();
            return _mapper.Map<TopicVm>(topic);
        }

        public async Task<TopicVm> GetById(Guid Id)
        {
            var query = from t in _managementDbContext.Topics
                        join c in _managementDbContext.Courses on t.CourseId equals c.Id
                        select new TopicVm()
                        {
                            Courses = _mapper.Map<Course, CourseVm>(c),
                            CreateTime = t.CreateTime,
                            Id = t.Id,
                            Description = t.Description,
                            Name = t.Name,
                            UpdateTime = t.UpdateTime,
                            CreatorId = t.CreatorId,
                            DeletorId = t.DeletorId,
                            UpdaterId = t.UpdaterId
                        };
            return await query.FirstOrDefaultAsync();
        }

        public async Task<PagedResultDto<TopicVm>> GetAllListAsync(PagedAndSortedResultRequestDto request)
        {
            var query = _managementDbContext.Topics;
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query.Where(x => x.Name.Contains(request.Filter));
            }
            if (string.IsNullOrEmpty(request.Sorting)) request.Sorting = nameof(Topic.Name);
            var tm = await query.OrderBy(x => x.Id).Skip((request.SkipCount - 1) * request.MaxResultCount).Take(request.MaxResultCount)
                .ToListAsync();

            return new PagedResultDto<TopicVm>
            {
                Items = _mapper.Map<List<TopicVm>>(tm),
                totalCount = query.Count()
            };
        }
    }
}