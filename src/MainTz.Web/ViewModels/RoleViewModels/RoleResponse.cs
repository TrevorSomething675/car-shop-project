using MainTz.Web.ViewModels.UserViewModels;

namespace MainTz.Web.ViewModels.RoleViewModels
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public ICollection<UserResponse> Users { get; set; }
        public string Name { get; set; }
    }
}