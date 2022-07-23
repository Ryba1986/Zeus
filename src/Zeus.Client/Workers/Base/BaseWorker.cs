using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Zeus.Client.Settings;

namespace Zeus.Client.Workers.Base
{
   internal abstract class BaseWorker : BackgroundService
   {
      protected readonly IMediator _mediator;
      protected readonly ZeusSettings _settings;

      public BaseWorker(IMediator mediator, ZeusSettings settings)
      {
         _mediator = mediator;
         _settings = settings;
      }

      protected Task GetPlcIntervalAsync(TimeSpan elapsedTime, CancellationToken cancellationToken)
      {
         TimeSpan intervalTime = TimeSpan.FromMilliseconds(_settings.PlcRefreshInterval);
         if (elapsedTime >= intervalTime)
         {
            return Task.CompletedTask;
         }

         return Task.Delay(intervalTime - elapsedTime, cancellationToken);
      }
   }
}
