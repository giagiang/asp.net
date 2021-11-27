using System;

namespace WebApplication.WebApi.ViewModels.Topics
{
    public class TopicUpdateDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Guid CourseId { set; get; }
    }
}