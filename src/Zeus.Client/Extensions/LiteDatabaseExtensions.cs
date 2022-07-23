using System.Collections.Generic;
using LiteDB;
using Zeus.Models.Base.Commands;

namespace Zeus.Client.Extensions
{
   internal static class LiteDatabaseExtensions
   {
      public static void InsertPlc<T>(this LiteDatabase database, T entity) where T : BaseCreatePlcCommand
      {
         database
            .GetCollection<T>()
            .Insert(entity);
      }

      public static void ReplaceAll<T>(this LiteDatabase database, IReadOnlyCollection<T> entities) where T : class
      {
         ILiteCollection<T> collection = database.GetCollection<T>();
         collection.DeleteAll();
         collection.Insert(entities);
      }
   }
}
