using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
    {
        public void Configure(EntityTypeBuilder<BrandEntity> builder)
        {
            builder.HasKey(b => b.Id);
            //builder.Property(b => b.Id).UseHiLo().UseIdentityColumn().ValueGeneratedOnAdd();

            builder.HasMany(b => b.Cars)
                .WithOne(c => c.Brand)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
