namespace MainTz.Web.ViewModels.UserViewModels
{
    public class UsersViewModel
    {
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<UserResponse> UsersResponse { get; set; }
    }
}