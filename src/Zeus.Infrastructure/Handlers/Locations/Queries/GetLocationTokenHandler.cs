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
   internal sealed class GetLocationTokenHandler : BaseRequestHandler, IRequestHandler<GetLocationTokenQuery, Result>
   {
      private readonly JwtSettings _settings;

      public GetLocationTokenHandler(UnitOfWork uow, JwtSettings settings) : base(uow)
      {
         _settings = settings;
      }

      public async Task<Result> Handle(GetLocationTokenQuery request, CancellationToken cancellationToken)
      {
         Location? existingLocation = await _uow.Location
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.MacAddress == request.MacAddress &&
               x.IsActive
            , cancellationToken);

         if (existingLocation is null)
         {
            return Result.Error("Location not found.");
         }

         // TODO: move to other logic
         bool clientInfoChanged =
            !string.IsNullOrWhiteSpace(request.Hostname) &&
            !string.IsNullOrWhiteSpace(request.ClientVersion) &&
            (existingLocation.Hostname != request.Hostname || existingLocation.ClientVersion != request.ClientVersion);

         if (clientInfoChanged)
         {
            existingLocation.Update(request.Hostname, request.ClientVersion);
            await _uow.Location.ReplaceOneAsync(x => x.Id == existingLocation.Id, existingLocation, cancellationToken: cancellationToken);
         }

         return JwtHandlerHelper.CreateClient(existingLocation.Id, _settings);
      }
   }
}
