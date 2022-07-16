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
   }
}
