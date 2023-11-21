using Microsoft.AspNetCore.Identity;

namespace MainTz.RestApi.dal.Data.Models.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
    }
}
