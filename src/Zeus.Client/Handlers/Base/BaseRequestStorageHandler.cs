using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using RestSharp;
using Zeus.Client.Extensions;
using Zeus.Models.Base;
using Zeus.Models.Base.Commands;

namespace Zeus.Client.Handlers.Base
{
   internal abstract class BaseRequestStorageHandler : BaseRequestHandler
   {
      protected readonly LiteDatabase _database;

      public BaseRequestStorageHandler(RestClient client, LiteDatabase database) : base(client)
      {
         _database = database;
      }

      protected async Task<Result> CreatePlcAsync<T>(string url, T entity, CancellationToken cancellationToken) where T : BaseCreatePlcCommand
      {
         Result result = await _client.PostAsync(url, entity, cancellationToken);
         if (result.IsSuccess)
         {
            await RestorePlcAsync<T>(url, cancellationToken);
         }
         else
         {
            _database.InsertPlc(entity);
         }

         return result;
      }

      private async Task RestorePlcAsync<T>(string url, CancellationToken cancellationToken) where T : BaseCreatePlcCommand
      {
         IReadOnlyCollection<T> plcs = _database.GetPlcAll<T>();
         foreach (T plc in plcs)
         {
            Result result = await _client.PostAsync(url, plc, cancellationToken);
            if (!result.IsSuccess)
            {
               continue;
            }

            _database.RemovePlc(plc);
         }
      }
   }
}
