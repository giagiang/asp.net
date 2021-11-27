using System;

namespace WebApplication.WebApi.ViewModels.Courses
{
    public class UpdateCourseDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime Start_Date { set; get; }
        public DateTime End_Date { set; get; }
    }
}