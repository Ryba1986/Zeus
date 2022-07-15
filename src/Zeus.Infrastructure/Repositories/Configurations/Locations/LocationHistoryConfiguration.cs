using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Locations;
using Zeus.Domain.Users;

namespace Zeus.Infrastructure.Repositories.Configurations.Locations
{
   internal sealed class LocationHistoryConfiguration : IEntityTypeConfiguration<LocationHistory>
   {
      public void Configure(EntityTypeBuilder<LocationHistory> builder)
      {
         builder.Property(x => x.Id)
            .IsRequired();

         builder.Property(x => x.LocationId)
            .IsRequired();

         builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

         builder.Property(x => x.MacAddress)
            .HasMaxLength(12)
            .IsRequired();

         builder.Property(x => x.IncludeReport)
            .IsRequired();

         builder.Property(x => x.IsActive)
           .IsRequired();

         builder.Property(x => x.CreatedById)
            .IsRequired();

         builder.Property(x => x.CreateDate)
            .IsRequired();

         builder.HasKey(x => x.Id)
            .IsClustered();

         builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(x => x.LocationId);

         builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedById);
      }
   }
}
