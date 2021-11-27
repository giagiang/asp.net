using System;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class ClassAssignRequest
    {
        public Guid UserId { get; set; }
        public Guid ClassId { get; set; }
    }
}