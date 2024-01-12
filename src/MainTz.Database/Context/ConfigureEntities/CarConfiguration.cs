using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Xml;

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

            builder.HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ModelId)
                .IsRequired(true);
        }
    }
}