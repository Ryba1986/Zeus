using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Meters;

namespace Zeus.Infrastructure.Repositories.Configurations.Plcs.Meters
{
   internal sealed class MeterConfiguration : IEntityTypeConfiguration<Meter>
   {
      public void Configure(EntityTypeBuilder<Meter> builder)
      {
         builder.Property(x => x.Date)
            .IsRequired();

         builder.Property(x => x.DeviceId)
            .IsRequired();

         builder.Property(x => x.InletTemp)
            .IsRequired();

         builder.Property(x => x.OutletTemp)
            .IsRequired();

         builder.Property(x => x.Power)
            .IsRequired();

         builder.Property(x => x.Volume)
            .IsRequired();

         builder.Property(x => x.VolumeSummary)
            .IsRequired();

         builder.Property(x => x.EnergySummary)
            .IsRequired();

         builder.Property(x => x.HourCount)
            .IsRequired();

         builder.Property(x => x.ErrorCode)
            .IsRequired();

         builder.HasKey(x => new { x.Date, x.DeviceId })
            .IsClustered();

         builder.HasOne<Device>()
            .WithMany()
            .HasForeignKey(x => x.DeviceId);
      }
   }
}
