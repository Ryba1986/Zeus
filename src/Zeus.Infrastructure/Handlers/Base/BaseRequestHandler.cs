using Zeus.Infrastructure.Repositories;

namespace Zeus.Infrastructure.Handlers.Base
{
   internal abstract class BaseRequestHandler
   {
      protected readonly UnitOfWork _uow;

      public BaseRequestHandler(UnitOfWork uow)
      {
         _uow = uow;
      }
   }
}
