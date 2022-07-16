using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zeus.Client.Settings;
using Zeus.Client.Workers.Base;
using Zeus.Models.Base;
using Zeus.Models.Locations.Queries;
using Zeus.Utilities.Extensions;
using Zeus.Utilities.Helpers;

namespace Zeus.Client.Workers
{
   internal sealed class AuthWorker : BaseWorker
   {
      public AuthWorker(IMediator mediator, ZeusSettings settings) : base(mediator, settings)
      {
      }

      protected override async Task ExecuteAsync(CancellationToken cancellationToken)
      {
         while (!cancellationToken.IsCancellationRequested)
         {
            Result result = await _mediator.Send(new GetLocationTokenRefreshQuery(), cancellationToken);
            if (!result.IsSuccess)
            {
               _ = await _mediator.Send(new GetLocationTokenQuery()
               {
                  MacAddress = NetworkHelper.GetMacAddress(),
                  ClientVersion = AssemblyExtensions.GetAssemblyVersion(GetType()),
                  Hostname = await NetworkHelper.GetTorHostAsync(_settings.TorHostPath, cancellationToken)
               }, cancellationToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
         }
      }
   }
}
