using Mapster;
using Zeus.Infrastructure.Repositories;

namespace Zeus.Infrastructure.Handlers.Base
{
   internal abstract class BaseRequestQueryHandler : BaseRequestHandler
   {
      protected readonly TypeAdapterConfig _mapper;

      public BaseRequestQueryHandler(UnitOfWork uow, TypeAdapterConfig mapper) : base(uow)
      {
         _mapper = mapper;
      }
   }
}
