using MediatR;
using Microsoft.Extensions.Hosting;

namespace Zeus.Client.Workers.Base
{
   internal abstract class BaseWorker : BackgroundService
   {
      protected readonly IMediator _mediator;

      public BaseWorker(IMediator mediator)
      {
         _mediator = mediator;
      }
   }
}
