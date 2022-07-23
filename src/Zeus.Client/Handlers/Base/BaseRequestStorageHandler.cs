using LiteDB;
using RestSharp;

namespace Zeus.Client.Handlers.Base
{
   internal abstract class BaseRequestStorageHandler : BaseRequestHandler
   {
      protected readonly LiteDatabase _database;

      public BaseRequestStorageHandler(RestClient client, LiteDatabase database) : base(client)
      {
         _database = database;
      }
   }
}
