using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Zeus.Models.Base.Commands;
using Zeus.Models.Base.Dto;

namespace Zeus.Client.Extensions
{
   internal static class LiteDatabaseExtensions
   {
      public static IReadOnlyCollection<T> GetPlcAll<T>(this LiteDatabase database, int limit = 50) where T : BaseCreatePlcCommand
      {
         return database
            .GetCollection<T>()
            .Query()
            .Limit(limit)
            .ToArray();
      }

      public static void InsertPlc<T>(this LiteDatabase database, T plc) where T : BaseCreatePlcCommand
      {
         database
            .GetCollection<T>()
            .Insert(plc);
      }

      public static void RemovePlc<T>(this LiteDatabase database, T plc) where T : BaseCreatePlcCommand
      {
         database
            .GetCollection<T>()
            .DeleteMany(x => x.Id == plc.Id);
      }

      public static void ReplaceAll<T>(this LiteDatabase database, IReadOnlyCollection<T> entities) where T : BaseDto
      {
         ILiteCollection<T> collection = database.GetCollection<T>();

         IReadOnlyCollection<string> entitiesVersions = entities
            .OrderBy(x => x.Id)
            .Select(x => Convert.ToHexString(x.Version))
            .ToArray();

         IReadOnlyCollection<string> storageVersions = collection
            .Query()
            .ToArray()
            .OrderBy(x => x.Id)
            .Select(x => Convert.ToHexString(x.Version))
            .ToArray();

         if (entitiesVersions.SequenceEqual(storageVersions))
         {
            return;
         }

         collection.DeleteAll();
         collection.Insert(entities);
      }
   }
}
