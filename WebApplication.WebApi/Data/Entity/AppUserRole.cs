using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.Data.Entity
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser AppUser { set; get; }
        public AppRole AppRole { set; get; }
    }
}