using System;
using System.Collections.Generic;
using WebApplication.WebApi.ViewModels.Users;

namespace WebApplication.WebApi.ViewModels.Classes
{
    public class ClassVm
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public Guid? Id { set; get; }
        public DateTime? CreateTime { set; get; }
        public Guid? CreatorId { set; get; }
        public DateTime? UpdateTime { set; get; }
        public Guid? UpdaterId { set; get; }
        public Guid? DeletorId { set; get; }
        public List<UserVm> User { set; get; }
    }
}