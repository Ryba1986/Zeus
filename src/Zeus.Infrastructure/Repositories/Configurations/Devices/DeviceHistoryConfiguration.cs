using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Devices;
using Zeus.Domain.Users;

namespace Zeus.Infrastructure.Repositories.Configurations.Devices
{
   internal sealed class DeviceHistoryConfiguration : IEntityTypeConfiguration<DeviceHistory>
   {
      public void Configure(EntityTypeBuilder<DeviceHistory> builder)
      {
         builder.Property(x => x.Id)
            .IsRequired();

         builder.Property(x => x.DeviceId)
            .IsRequired();

         builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();

         builder.Property(x => x.LocationName)
            .HasMaxLength(30)
            .IsRequired();

         builder.Property(x => x.Type)
            .HasMaxLength(30)
            .IsRequired();

         builder.Property(x => x.Name)
            .HasMaxLength(3)
            .IsRequired();

         builder.Property(x => x.RsBoundRate)
            .HasMaxLength(10)
            .IsRequired();

         builder.Property(x => x.RsDataBits)
            .HasMaxLength(5)
            .IsRequired();

         builder.Property(x => x.RsParity)
            .HasMaxLength(10)
            .IsRequired();

         builder.Property(x => x.RsStopBits)
            .HasMaxLength(10)
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

         builder.HasOne<Device>()
            .WithMany()
            .HasForeignKey(x => x.DeviceId);

         builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedById);
      }
   }
}
