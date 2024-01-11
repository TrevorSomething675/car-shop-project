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
            //builder.Property(car => car.Id)
            //    .ValueGeneratedOnAdd()
            //    .HasAnnotation("DataBaseGenerated", "MyCustomIdGenerator");

            //builder.HasMany(c => c.Users)
            //    .WithMany(u => u.Cars).UsingEntity("UserEntity");

            //builder.HasMany(c => c.Users)
            //    .WithOne(u => u.Car)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasForeignKey(uc => uc.CarId)
            //    .IsRequired(false);

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
    //public class MyCustomIdGenerator : ValueGenerator<int>
    //{
    //    public override int Next(EntityEntry entry)
    //    {
    //        int generatedId = GenerateUniqueId();

    //        var dbContext = entry.Context;
    //        var existingEntity = dbContext.Set<CarEntity>().Find(generatedId);
    //        if (existingEntity != null)
    //        {
    //            generatedId = GenerateUniqueId();
    //        }

    //        return generatedId;
    //    }

    //    public override bool GeneratesTemporaryValues => false;

    //    private int GenerateUniqueId()
    //    {
    //        return new Random().Next();
    //    }
    //}
}