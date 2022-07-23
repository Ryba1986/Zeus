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
   }
}
