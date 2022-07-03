using AutoMapper;
using Zeus.Infrastructure.Repositories;

namespace Zeus.Infrastructure.Handlers.Base
{
   internal abstract class BaseRequestQueryHandler : BaseRequestHandler
   {
      protected readonly IMapper _mapper;

      public BaseRequestQueryHandler(UnitOfWork uow, IMapper mapper) : base(uow)
      {
         _mapper = mapper;
      }
   }
}
