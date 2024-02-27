using System.ComponentModel.DataAnnotations;

namespace E_CarShop.DataBase.Entities
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}