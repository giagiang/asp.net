using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.WebApi.ViewModels.Common

{
    public class PagedResultDto<T>
    {
        public List<T> Items { set; get; }
        public int totalCount { set; get; }
    }
}