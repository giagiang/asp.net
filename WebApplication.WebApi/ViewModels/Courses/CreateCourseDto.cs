using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.ViewModels.Courses
{
    public class CreateCourseDto
    {
        public string Name { set; get; }
        public string Description { set; get; }
    }
}