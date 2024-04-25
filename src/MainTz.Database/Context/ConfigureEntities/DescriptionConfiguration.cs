using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class DescriptionConfiguration : IEntityTypeConfiguration<DescriptionEntity>
    {
        public void Configure(EntityTypeBuilder<DescriptionEntity> builder)
        {
            builder.HasOne(d => d.Car)
                .WithOne(c => c.Description);
        }
    }
}
