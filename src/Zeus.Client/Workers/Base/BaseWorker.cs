using MediatR;
using Microsoft.Extensions.Hosting;

namespace Zeus.Client.Workers.Base
{
   internal abstract class BaseWorker : BackgroundService
   {
      protected readonly Mediator _mediator;

      public BaseWorker(Mediator mediator)
      {
         _mediator = mediator;
      }
   }
}
