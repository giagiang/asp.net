using System;
using System.Collections.Generic;
using WebApplication.WebApi.ViewModels.Classes;

namespace WebApplication.WebApi.ViewModels.Users
{
    public class UserClassVm
    {
        public Guid Id { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public IList<string> Roles { set; get; }
        public List<ClassVm> Classes { set; get; }
    }
}