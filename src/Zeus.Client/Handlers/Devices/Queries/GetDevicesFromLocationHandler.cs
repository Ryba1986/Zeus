using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using MediatR;
using RestSharp;
using Zeus.Client.Extensions;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Devices.Queries;

namespace Zeus.Client.Handlers.Devices.Queries
{
   internal sealed class GetDevicesFromLocationHandler : BaseRequestStorageHandler, IRequestHandler<GetDevicesFromLocationQuery, IReadOnlyCollection<DeviceDto>>
   {
      public GetDevicesFromLocationHandler(RestClient client, LiteDatabase database) : base(client, database)
      {
      }

      public async Task<IReadOnlyCollection<DeviceDto>> Handle(GetDevicesFromLocationQuery request, CancellationToken cancellationToken)
      {
         IReadOnlyCollection<DeviceDto>? result = await _client.GetAsync("device/getDevicesFromLocation", request, cancellationToken);
         if (result is null)
         {
            return _database.GetCollection<DeviceDto>()
               .Query()
               .ToArray();
         }

         // TODO: update only when configuration changes
         _database.ReplaceAll(result);
         return result;
      }
   }
}
