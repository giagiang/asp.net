using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.Data.Entity
{
    public class Course : BaseEntity
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime Start_Date { set; get; }
        public DateTime End_Date { set; get; }
        public string Image { set; get; }
        public List<Topic> Topics { set; get; }
        public List<CourseCategory> CourseCategories { set; get; }
        public List<UserCourse> UserCourses { set; get; }
    }
}