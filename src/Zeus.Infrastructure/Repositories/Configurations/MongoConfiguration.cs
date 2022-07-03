using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Locations;
using Zeus.Domain.Users;
using Zeus.Enums.Users;
using Zeus.Utilities.Extensions;

namespace Zeus.Infrastructure.Repositories.Configurations
{
   internal static class MongoConfiguration
   {
      public static async Task SetupAsync(IMongoDatabase database)
      {
         await CreateIndexesAsync(database);
         await CreateDataAsync(database);
      }

      private static async Task CreateIndexesAsync(IMongoDatabase database)
      {
         await LocationIndexesAsync(database);
         await LocationHistoryIndexesAsync(database);

         await CreateUserIndexesAsync(database);
         await CreateUserHistoryIndexesAsync(database);
      }

      private static Task LocationIndexesAsync(IMongoDatabase database)
      {
         List<CreateIndexModel<Location>> list = new()
         {
            new CreateIndexModel<Location>(
               Builders<Location>.IndexKeys.Ascending(x => x.Name),
               new CreateIndexOptions { Name = nameof(Location.Name), Unique = true, Background = true }),

            new CreateIndexModel<Location>(
               Builders<Location>.IndexKeys.Ascending(x => x.MacAddress),
               new CreateIndexOptions { Name = nameof(Location.MacAddress), Unique = true, Background = true })
         };

         return CreateIndexesAsync(list, database);
      }

      private static Task LocationHistoryIndexesAsync(IMongoDatabase database)
      {
         List<CreateIndexModel<LocationHistory>> list = new()
         {
            new CreateIndexModel<LocationHistory>(Builders<LocationHistory>.IndexKeys.Combine(
               Builders<LocationHistory>.IndexKeys.Ascending(x => x.LocationId),
               Builders<LocationHistory>.IndexKeys.Ascending(x => x.CreateDate)
            ),
            new CreateIndexOptions { Name = $"{nameof(LocationHistory.LocationId)}-{nameof(LocationHistory.CreateDate)}", Unique = true, Background = true })
         };

         return CreateIndexesAsync(list, database);
      }

      private static Task CreateUserIndexesAsync(IMongoDatabase database)
      {
         List<CreateIndexModel<User>> list = new()
         {
            new CreateIndexModel<User>(
               Builders<User>.IndexKeys.Ascending(x => x.Email),
               new CreateIndexOptions { Name = nameof(User.Email), Unique = true, Background = true })
         };

         return CreateIndexesAsync(list, database);
      }

      private static Task CreateUserHistoryIndexesAsync(IMongoDatabase database)
      {
         List<CreateIndexModel<UserHistory>> list = new()
         {
            new CreateIndexModel<UserHistory>(Builders<UserHistory>.IndexKeys.Combine(
               Builders<UserHistory>.IndexKeys.Ascending(x => x.UserId),
               Builders<UserHistory>.IndexKeys.Ascending(x => x.CreateDate)
            ),
            new CreateIndexOptions { Name = $"{nameof(UserHistory.UserId)}-{nameof(UserHistory.CreateDate)}", Unique = true, Background = true })
         };

         return CreateIndexesAsync(list, database);
      }

      private static async Task CreateDataAsync(IMongoDatabase database)
      {
         await CreateUserDataAsync(database);
      }

      private static async Task CreateUserDataAsync(IMongoDatabase database)
      {
         IMongoCollection<User> users = database.GetCollection<User>(nameof(User));
         IMongoCollection<UserHistory> history = database.GetCollection<UserHistory>(nameof(UserHistory));

         User newUser = new("Administrator", "admin@admin.com", "admin@admin.com", UserRole.SysAdmin, true);

         User existingUser = await users
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Email == newUser.Email
            );

         if (existingUser is not null)
         {
            return;
         }

         await users.InsertOneAsync(newUser);
         await history.InsertOneAsync(new(newUser.Id, newUser.Name, newUser.Email, newUser.Role.GetDescription(), newUser.IsActive, newUser.Id));
      }

      private static Task CreateIndexesAsync<T>(IEnumerable<CreateIndexModel<T>> indexModel, IMongoDatabase database) where T : class
      {
         IMongoCollection<T> collection = database.GetCollection<T>(typeof(T).Name);
         return collection.Indexes.CreateManyAsync(indexModel);
      }

   }
}
