using System;

namespace WebApplication.WebApi.ViewModels.Classes
{
    public class UpdateClassDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
    }
}