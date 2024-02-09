using MainTz.Database.Context.ConfigureEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MainTz.Database.Entities;
using MainTz.Core.Options;

namespace MainTa.Database.Context
{
    public class MainContext : DbContext
    {
        private readonly DataBaseOptions _dataBaseSettings;
        public MainContext(DbContextOptions<MainContext> options, 
            IOptions<DataBaseOptions> dataBaseSettings) : base(options) 
        {
            _dataBaseSettings = dataBaseSettings.Value;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }

        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ModelEntity> Models { get; set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dataBaseSettings.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}