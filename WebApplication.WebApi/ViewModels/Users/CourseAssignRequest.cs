using System;
using System.Collections.Generic;
using WebApplication.WebApi.ViewModels.Common;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class CourseAssignRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}