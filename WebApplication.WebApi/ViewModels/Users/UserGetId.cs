using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class UserGetId
    {
        public Guid Id { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public string ImagePath { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public IList<string> Roles { set; get; }
    }
}