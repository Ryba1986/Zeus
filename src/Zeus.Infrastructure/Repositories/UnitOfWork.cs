using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Zeus.Domain.Devices;
using Zeus.Domain.Locations;
using Zeus.Domain.Plcs.Meters;
using Zeus.Domain.Plcs.Rvds;
using Zeus.Domain.Users;
using Zeus.Models.Base;

namespace Zeus.Infrastructure.Repositories
{
   internal sealed class UnitOfWork
   {
      public IMongoCollection<Location> Location { get; init; }
      public IMongoCollection<LocationHistory> LocationHistory { get; init; }

      public IMongoCollection<Device> Device { get; init; }
      public IMongoCollection<DeviceHistory> DeviceHistory { get; init; }

      public IMongoCollection<User> User { get; init; }
      public IMongoCollection<UserHistory> UserHistory { get; init; }

      public IMongoCollection<Meter> Meter { get; init; }
      public IMongoCollection<Rvd145> Rvd145 { get; init; }

      private readonly IMongoDatabase _database;

      public UnitOfWork(IMongoDatabase database)
      {
         _database = database;

         Location = _database.GetCollection<Location>(nameof(Location));
         LocationHistory = _database.GetCollection<LocationHistory>(nameof(LocationHistory));

         Device = _database.GetCollection<Device>(nameof(Device));
         DeviceHistory = _database.GetCollection<DeviceHistory>(nameof(DeviceHistory));

         User = _database.GetCollection<User>(nameof(User));
         UserHistory = _database.GetCollection<UserHistory>(nameof(UserHistory));

         Meter = _database.GetCollection<Meter>(nameof(Meter));
         Rvd145 = _database.GetCollection<Rvd145>(nameof(Rvd145));
      }

      public async Task<Result> ExecuteTransaction(Func<IClientSessionHandle, CancellationToken, Task> action, CancellationToken cancellationToken)
      {
         using IClientSessionHandle session = await _database.Client.StartSessionAsync(cancellationToken: cancellationToken);
         try
         {
            session.StartTransaction();
            await action(session, cancellationToken);
            await session.CommitTransactionAsync(cancellationToken);

            return Result.Success();
         }
         catch (Exception ex)
         {
            await session.AbortTransactionAsync(cancellationToken);
            return Result.Error($"Transaction error: {ex.Message}");
         }
      }
   }
}
