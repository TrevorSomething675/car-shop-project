using Microsoft.AspNetCore.Identity;

namespace MainTz.RestApi.dal.Data.Models.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
    }
}
