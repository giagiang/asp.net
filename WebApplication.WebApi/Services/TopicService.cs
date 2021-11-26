using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.Data.DbContext;
using WebApplication.WebApi.Data.Entity;
using WebApplication.WebApi.ViewModels.Courses;
using WebApplication.WebApi.ViewModels.Topics;

namespace WebApplication.WebApi.Services
{
    public interface ITopicService
    {
        Task<TopicVm> CreateAsync(TopicCreateDto dto);

        Task<List<TopicVm>> GetListAsync();

        Task<bool> DeleteAsync(Guid Id);
    }

    public class TopicService : ITopicService
    {
        private readonly ManagementDbContext _managementDbContext;
        private readonly IMapper _mapper;

        public TopicService(ManagementDbContext managementDbContext, IMapper mapper)
        {
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

        public async Task<List<TopicVm>> GetListAsync()
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

            return await query.ToListAsync();
        }
    }
}