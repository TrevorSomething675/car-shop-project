using Extensions;

namespace MainTz.RestApi.dal.Data.Models.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
	}
}