using MainTz.Web.ViewModels.UserViewModels;

namespace MainTz.Web.ViewModels.RoleViewModels
{
    public class RoleRequest
    {
        public int Id { get; set; }
        public ICollection<UserRequest> Users { get; set; }
        public string Name { get; set; }
    }
}