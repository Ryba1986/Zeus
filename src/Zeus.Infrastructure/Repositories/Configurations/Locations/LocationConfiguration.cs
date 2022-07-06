using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Locations;

namespace Zeus.Infrastructure.Repositories.Configurations.Locations
{
   internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
   {
      public void Configure(EntityTypeBuilder<Location> builder)
      {
         builder.Property(x => x.Id)
            .IsRequired();

         builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();

         builder.Property(x => x.MacAddress)
            .HasMaxLength(12)
            .IsFixedLength()
            .IsRequired();

         builder.Property(x => x.Hostname)
            .HasMaxLength(62)
            .IsFixedLength()
            .IsRequired();

         builder.Property(x => x.ClientVersion)
            .HasMaxLength(20)
            .IsRequired();

         builder.Property(x => x.IncludeReport)
            .IsRequired();

         builder.Property(x => x.IsActive)
            .IsRequired();

         builder.Property(x => x.Version)
            .IsRequired()
            .IsRowVersion();

         builder.HasKey(x => x.Id)
            .IsClustered();
      }
   }
}
