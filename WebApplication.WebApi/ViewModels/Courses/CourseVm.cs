using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.ViewModels.Courses
{
    public class CourseVm
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime Start_Date { set; get; }
        public DateTime End_Date { set; get; }
        public Guid? Id { set; get; }
        public DateTime? CreateTime { set; get; }
        public Guid? CreatorId { set; get; }
        public DateTime? UpdateTime { set; get; }
        public Guid? UpdaterId { set; get; }
        public Guid? DeletorId { set; get; }
    }
}