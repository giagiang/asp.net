using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class RoleVm
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public List<UserVm> Users { set; get; }
    }
}