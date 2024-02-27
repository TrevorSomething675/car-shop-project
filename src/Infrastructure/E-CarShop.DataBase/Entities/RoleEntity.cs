namespace E_CarShop.DataBase.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
