using Microsoft.EntityFrameworkCore;

namespace MainTz.RestApi.dal.Data.Models.Entities
{
    public class MainContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        public MainContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}