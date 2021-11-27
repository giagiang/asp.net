using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.WebApi.ViewModels.Common;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}