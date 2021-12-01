using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.Data.Entity
{
    public class CourseCategory
    {
        public Course Course { set; get; }
        public Guid CourseId { set; get; }
        public Category Category { set; get; }
        public Guid CategoryId { set; get; }
        public DateTime CreateTime { set; get; }
        public Guid CreatorId { set; get; }
        public DateTime UpdateTime { set; get; }
    }
}