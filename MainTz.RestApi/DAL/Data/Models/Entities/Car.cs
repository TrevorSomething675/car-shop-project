using MainTz.RestApi.dal.Data;

namespace MainTz.RestApi.DAL.Data.Models.Entities
{
    public class Car : BaseEntity
    {
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }

        public int UserId { get; set; }
        public ICollection<User> Users { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }
    }
}