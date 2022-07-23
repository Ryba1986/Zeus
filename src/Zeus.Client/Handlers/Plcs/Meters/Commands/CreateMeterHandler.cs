using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using MediatR;
using RestSharp;
using Zeus.Client.Extensions;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Meters.Commands;

namespace Zeus.Client.Handlers.Plcs.Meters.Commands
{
   internal sealed class CreateMeterHandler : BaseRequestStorageHandler, IRequestHandler<CreateMeterCommand, Result>
   {
      public CreateMeterHandler(RestClient client, LiteDatabase database) : base(client, database)
      {
      }

      public async Task<Result> Handle(CreateMeterCommand request, CancellationToken cancellationToken)
      {
         Result result = await _client.PostAsync("meter/createMeter", request, cancellationToken);
         if (!result.IsSuccess)
         {
            _database.InsertPlc(request);
         }

         return result;
      }
   }
}
