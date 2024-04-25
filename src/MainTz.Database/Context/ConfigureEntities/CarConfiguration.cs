using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
    {
        public void Configure(EntityTypeBuilder<CarEntity> builder)
        {
            builder.Property(car => car.Id).IsRequired();

            builder.HasMany(c => c.Images)
                .WithOne(img => img.Car)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(img => img.CarId)
                .IsRequired(false);

            builder.HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId)
                .IsRequired(true);

            builder.HasOne(c => c.Manufacturer)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ManufacturerId)
                .IsRequired(true);

            builder.HasOne(c => c.Description)
                .WithOne(d => d.Car);
        }
    }
}