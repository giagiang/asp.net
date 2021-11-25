namespace WebApplication.WebApi.ViewModels.Users
{
    public class UpdateUserDto
    {
        public string FullName { set; get; }
        public string Address { set; get; }
        public string ImagePath { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
    }
}