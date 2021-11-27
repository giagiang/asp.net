using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.Data.Entity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { set; get; }
        public List<UserCourse> UserCourses { set; get; }
        public List<UserClass> UserClasses { set; get; }
    }
}