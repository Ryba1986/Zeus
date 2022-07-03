using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Repositories;
using Zeus.Models.Locations.Queries;

namespace Zeus.Infrastructure.Handlers.Locations.Queries
{
   internal sealed class GetLocationsDictionaryHandler : BaseRequestHandler, IRequestHandler<GetLocationsDictionaryQuery, IEnumerable<KeyValuePair<int, string>>>
   {
      public GetLocationsDictionaryHandler(UnitOfWork uow) : base(uow)
      {
      }

      public async Task<IEnumerable<KeyValuePair<int, string>>> Handle(GetLocationsDictionaryQuery request, CancellationToken cancellationToken)
      {
         return await _uow.Location
            .AsQueryable()
            .OrderBy(x => x.Name)
            .Select(x => new KeyValuePair<int, string>(x.Id, x.Name))
            .ToListAsync(cancellationToken);
      }
   }
}
