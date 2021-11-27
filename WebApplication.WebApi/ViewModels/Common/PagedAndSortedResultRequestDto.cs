namespace WebApplication.WebApi.ViewModels.Common
{
    public class PagedAndSortedResultRequestDto
    {
        public string Sorting { set; get; }
        public int SkipCount { set; get; } = 1;

        public int MaxResultCount { set; get; } = 10;
        public string Filter { set; get; }
    }
}