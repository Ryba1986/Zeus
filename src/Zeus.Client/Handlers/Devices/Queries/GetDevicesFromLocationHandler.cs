using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using Zeus.Client.Extensions;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Devices.Dto;
using Zeus.Models.Devices.Queries;

namespace Zeus.Client.Handlers.Devices.Queries
{
   internal sealed class GetDevicesFromLocationHandler : BaseRequestHandler, IRequestHandler<GetDevicesFromLocationQuery, IReadOnlyCollection<DeviceDto>>
   {
      public GetDevicesFromLocationHandler(RestClient client) : base(client)
      {
      }

      public async Task<IReadOnlyCollection<DeviceDto>> Handle(GetDevicesFromLocationQuery request, CancellationToken cancellationToken)
      {
         IReadOnlyCollection<DeviceDto>? result = await _client.GetAsync("device/getDevicesFromLocation", request, cancellationToken);
         if (result is null)
         {
            // TODO: add local storage logic
            return Array.Empty<DeviceDto>();
         }

         return result;
      }
   }
}
