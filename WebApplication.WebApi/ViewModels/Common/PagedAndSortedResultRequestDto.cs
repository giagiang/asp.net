namespace WebApplication.WebApi.ViewModels.Common
{
    public class PagedAndSortedResultRequestDto
    {
        public string Sorting { set; get; }
        public int SkipCount { set; get; }
        public int MaxResultCount { set; get; }
        public string Filter { set; get; }
    }
}