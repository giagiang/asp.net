using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.ViewModels.Courses;

namespace WebApplication.WebApi.ViewModels.Topics
{
    public class TopicVm
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime? CreateTime { set; get; }
        public Guid? CreatorId { set; get; }
        public DateTime? UpdateTime { set; get; }
        public Guid? UpdaterId { set; get; }
        public Guid? DeletorId { set; get; }
        public Guid? Id { set; get; }
        public CourseVm Courses { set; get; }
    }
}