using Microsoft.EntityFrameworkCore;
using Zeus.Domain.Devices;
using Zeus.Domain.Locations;
using Zeus.Domain.Plcs.Meters;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Domain.Users;

namespace Zeus.Infrastructure.Repositories
{
   internal sealed class UnitOfWork : DbContext
   {
      public DbSet<Location> Location { get; init; }
      public DbSet<LocationHistory> LocationHistory { get; init; }

      public DbSet<Device> Device { get; init; }
      public DbSet<DeviceHistory> DeviceHistory { get; init; }

      public DbSet<User> User { get; init; }
      public DbSet<UserHistory> UserHistory { get; init; }

      public DbSet<Meter> Meter { get; init; }
      public DbSet<Rvd145> Rvd145 { get; init; }

      public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options)
      {
         Location = Set<Location>();
         LocationHistory = Set<LocationHistory>();

         Device = Set<Device>();
         DeviceHistory = Set<DeviceHistory>();

         User = Set<User>();
         UserHistory = Set<UserHistory>();

         Meter = Set<Meter>();
         Rvd145 = Set<Rvd145>();
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
      }
   }
}
