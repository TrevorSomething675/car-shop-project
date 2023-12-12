using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Role)
                .WithMany(r => r.User)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Cars)
                .WithMany(c => c.Users);
        }
    }
}