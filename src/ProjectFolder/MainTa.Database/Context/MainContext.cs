using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTa.Database.Context
{
    public class MainContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }

        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ModelEntity> Models { get; set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        public MainContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
