using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<ManufacturerEntity>
    {
        public void Configure(EntityTypeBuilder<ManufacturerEntity> builder)
        {
            builder.HasMany(m => m.Cars)
                .WithOne(c => c.Manufacturer)
                .HasForeignKey(c => c.ManufacturerId)
                .IsRequired(true);
        }
    }
}
