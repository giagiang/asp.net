using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.Data.Entity
{
    public class UserCourse
    {
        public AppUser AppUser { set; get; }
        public Guid? UserId { set; get; }
        public Course Course { set; get; }
        public Guid? CourseId { set; get; }
        public DateTime? CreateTime { set; get; }
        public Guid? CreatorId { set; get; }
        public DateTime? UpdateTime { set; get; }
        public Guid? UpdaterId { set; get; }
    }
}