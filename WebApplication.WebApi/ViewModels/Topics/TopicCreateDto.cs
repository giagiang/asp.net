using System;

namespace WebApplication.WebApi.ViewModels.Topics
{
    public class TopicCreateDto
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public Guid CourseId { set; get; }
    }
}