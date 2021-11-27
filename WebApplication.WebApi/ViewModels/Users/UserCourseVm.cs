using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.ViewModels.Courses;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class UserCourseVm
    {
        public Guid Id { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public IList<string> Roles { set; get; }
        public List<CourseVm> Courses { set; get; }
    }
}