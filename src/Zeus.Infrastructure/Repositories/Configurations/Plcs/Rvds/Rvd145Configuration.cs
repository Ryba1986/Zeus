using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Rvds;

namespace Zeus.Infrastructure.Repositories.Configurations.Plcs.Rvds
{
   internal sealed class Rvd145Configuration : IEntityTypeConfiguration<Rvd145>
   {
      public void Configure(EntityTypeBuilder<Rvd145> builder)
      {
         builder.Property(x => x.Date)
            .IsRequired();

         builder.Property(x => x.DeviceId)
            .IsRequired();

         builder.Property(x => x.OutsideTemp)
            .IsRequired();

         builder.Property(x => x.CoHighInletPresure)
            .IsRequired();

         builder.Property(x => x.Alarm)
            .IsRequired();

         builder.Property(x => x.Co1HighOutletTemp)
            .IsRequired();

         builder.Property(x => x.Co1LowInletTemp)
            .IsRequired();

         builder.Property(x => x.Co1LowOutletPresure)
            .IsRequired();

         builder.Property(x => x.Co1HeatCurveTemp)
            .IsRequired();

         builder.Property(x => x.Co1PumpStatus)
            .IsRequired();

         builder.Property(x => x.Co1Status)
            .IsRequired();

         builder.Property(x => x.CwuTemp)
            .IsRequired();

         builder.Property(x => x.CwuTempSet)
            .IsRequired();

         builder.Property(x => x.CwuCirculationTemp)
            .IsRequired();

         builder.Property(x => x.CwuPumpStatus)
            .IsRequired();

         builder.Property(x => x.CwuStatus)
            .IsRequired();

         builder.HasKey(x => new { x.Date, x.DeviceId })
            .IsClustered();

         builder.HasOne<Device>()
            .WithMany()
            .HasForeignKey(x => x.DeviceId);
      }
   }
}
