using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MainTz.RestApi.Data
{
    public class BaseEntity
    {
        [Column("Id"), Required]
        public int Id { get; set; }
    }
}
