using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.Data.Entity
{
    public class Category : BaseEntity
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public List<CourseCategory> CourseCategories { set; get; }
    }
}