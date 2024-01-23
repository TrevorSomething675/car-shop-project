using System.ComponentModel.DataAnnotations.Schema;

namespace MainTz.Database.Entities
{
    public class BaseEntity
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
    }
}
