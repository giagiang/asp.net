using System;
using System.Collections.Generic;
using WebApplication.WebApi.ViewModels.Classes;
using WebApplication.WebApi.ViewModels.Courses;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class UserVm
    {
        public Guid Id { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public string ImagePath { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public List<ClassVm> Class { set; get; }
        public List<CourseVm> Course { set; get; }
        public IList<string> Roles { set; get; }
    }
}