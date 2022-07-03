using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Locations;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Helpers;
using Zeus.Infrastructure.Repositories;
using Zeus.Infrastructure.Settings;
using Zeus.Models.Base;
using Zeus.Models.Locations.Queries;

namespace Zeus.Infrastructure.Handlers.Locations.Queries
{
   internal sealed class GetLocationTokenRefreshHandler : BaseRequestHandler, IRequestHandler<GetLocationTokenRefreshQuery, Result>
   {
      private readonly JwtSettings _settings;

      public GetLocationTokenRefreshHandler(UnitOfWork uow, JwtSettings settings) : base(uow)
      {
         _settings = settings;
      }

      public async Task<Result> Handle(GetLocationTokenRefreshQuery request, CancellationToken cancellationToken)
      {
         Location? existingLocation = await _uow.Location
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Id == request.LocationId &&
               x.IsActive
            , cancellationToken);

         if (existingLocation is null)
         {
            return Result.Error("Location not found.");
         }

         return JwtHandlerHelper.CreateClient(existingLocation.Id, _settings);
      }
   }
}
