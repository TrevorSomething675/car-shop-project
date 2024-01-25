namespace MainTz.Web.ViewModels.UserViewModels
{
    public class GetUsersRequest
    {
        public int PageNumber { get; set; } = 1;
        public bool IsSortedByRole { get; set; } = false;
    }
}
