using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Devices;
using Zeus.Domain.Locations;

namespace Zeus.Infrastructure.Repositories.Configurations.Devices
{
   internal sealed class DeviceConfiguration : IEntityTypeConfiguration<Device>
   {
      public void Configure(EntityTypeBuilder<Device> builder)
      {
         builder.Property(x => x.Id)
            .IsRequired();

         builder.Property(x => x.LocationId)
            .IsRequired();

         builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();

         builder.Property(x => x.SerialNumber)
         .HasMaxLength(30)
         .IsRequired();

         builder.Property(x => x.Type)
            .IsRequired();

         builder.Property(x => x.ModbusId)
            .IsRequired();

         builder.Property(x => x.RsBoundRate)
            .IsRequired();

         builder.Property(x => x.RsDataBits)
            .IsRequired();

         builder.Property(x => x.RsParity)
            .IsRequired();

         builder.Property(x => x.RsStopBits)
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

         builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(x => x.LocationId);
      }
   }
}
