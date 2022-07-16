using System.Diagnostics;
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

      protected int GetPlcRefreshInterval(Stopwatch sw)
      {
         int diff = _settings.PlcRefreshInterval - (int)sw.ElapsedMilliseconds;
         if (diff < 0)
         {
            return 0;
         }

         return diff;
      }
   }
}
