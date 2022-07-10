using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Devices;
using Zeus.Domain.Plcs.Climatixs;

namespace Zeus.Infrastructure.Repositories.Configurations.Plcs.Climatixs
{
   internal sealed class ClimatixConfiguration : IEntityTypeConfiguration<Climatix>
   {
      public void Configure(EntityTypeBuilder<Climatix> builder)
      {
         builder.Property(x => x.Date)
            .IsRequired();

         builder.Property(x => x.DeviceId)
            .IsRequired();

         builder.Property(x => x.OutsideTemp)
            .IsRequired();

         builder.Property(x => x.CoHighInletPresure)
            .IsRequired();

         builder.Property(x => x.CoHighOutletPresure)
            .IsRequired();

         builder.Property(x => x.Alarm)
            .IsRequired();

         builder.Property(x => x.Co1LowInletTemp)
            .IsRequired();

         builder.Property(x => x.Co1LowOutletTemp)
            .IsRequired();

         builder.Property(x => x.Co1LowOutletPresure)
            .IsRequired();

         builder.Property(x => x.Co1HeatCurveTemp)
            .IsRequired();

         builder.Property(x => x.Co1PumpAlarm)
            .IsRequired();

         builder.Property(x => x.Co1PumpStatus)
            .IsRequired();

         builder.Property(x => x.Co1ValvePosition)
            .IsRequired();

         builder.Property(x => x.Co1Status)
            .IsRequired();

         builder.Property(x => x.Co2LowInletTemp)
            .IsRequired();

         builder.Property(x => x.Co2LowOutletTemp)
            .IsRequired();

         builder.Property(x => x.Co2LowOutletPresure)
            .IsRequired();

         builder.Property(x => x.Co2HeatCurveTemp)
            .IsRequired();

         builder.Property(x => x.Co2PumpAlarm)
            .IsRequired();

         builder.Property(x => x.Co2PumpStatus)
            .IsRequired();

         builder.Property(x => x.Co2ValvePosition)
            .IsRequired();

         builder.Property(x => x.Co2Status)
            .IsRequired();

         builder.Property(x => x.CwuTemp)
            .IsRequired();

         builder.Property(x => x.CwuTempSet)
            .IsRequired();

         builder.Property(x => x.CwuPumpAlarm)
            .IsRequired();

         builder.Property(x => x.CwuPumpStatus)
            .IsRequired();

         builder.Property(x => x.CwuValvePosition)
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
