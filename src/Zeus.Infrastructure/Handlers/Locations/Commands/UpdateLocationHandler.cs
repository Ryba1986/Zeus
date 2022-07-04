using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Zeus.Domain.Locations;
using Zeus.Domain.Users;
using Zeus.Infrastructure.Handlers.Base;
using Zeus.Infrastructure.Helpers;
using Zeus.Infrastructure.Repositories;
using Zeus.Infrastructure.Settings;
using Zeus.Models.Base;
using Zeus.Models.Locations.Commands;

namespace Zeus.Infrastructure.Handlers.Locations.Commands
{
   internal sealed class UpdateLocationHandler : BaseRequestHandler, IRequestHandler<UpdateLocationCommand, Result>
   {
      private readonly ZeusSettings _settings;

      public UpdateLocationHandler(UnitOfWork uow, ZeusSettings settings) : base(uow)
      {
         _settings = settings;
      }

      public async Task<Result> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
      {
         User? modifiedBy = await _uow.User
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Id == request.ModifiedById &&
               x.IsActive
            , cancellationToken);

         if (modifiedBy is null)
         {
            return Result.Error($"Cannot find an active user with id {request.ModifiedById}.");
         }

         Location? otherLocation = await _uow.Location
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               (x.Name == request.Name || x.MacAddress == request.MacAddress) &&
               x.Id != request.Id
            , cancellationToken);

         if (otherLocation?.Name == request.Name)
         {
            return Result.Error($"Location with name {request.Name} exist.");
         }
         if (otherLocation?.MacAddress == request.MacAddress)
         {
            return Result.Error($"Location with mac address {request.MacAddress} exist.");
         }

         Location? existingLocation = await _uow.Location
            .AsQueryable()
            .FirstOrDefaultAsync(x =>
               x.Id == request.Id
            , cancellationToken);

         if (existingLocation == null)
         {
            return Result.Error("Location not exist.");
         }
         if (existingLocation.Version != request.Version)
         {
            return Result.Error("Location has been changed.");
         }
         if (!existingLocation.IsActive && request.IsActive && await LocationHandlerHelper.IsLimitLocations(_uow, _settings.LocationLimit, cancellationToken))
         {
            return Result.Error("The limit of active locations has been exceeded.");
         }

         return await _uow.ExecuteTransactionAsync(
            async (session, token) =>
            {
               await CreateHistoryAsync(session, existingLocation, request, modifiedBy, token);

               existingLocation.Update(request.Name, request.MacAddress, request.IncludeReport, request.IsActive);
               await _uow.Location.ReplaceOneAsync(session, x => x.Id == existingLocation.Id, existingLocation, cancellationToken: token);
            },
            cancellationToken
         );
      }

      private async Task CreateHistoryAsync(IClientSessionHandle session, Location location, UpdateLocationCommand request, User createdBy, CancellationToken cancellationToken)
      {
         if (
               location.Name == request.Name &&
               location.MacAddress == request.MacAddress &&
               location.IsActive == request.IsActive &&
               location.IncludeReport == request.IncludeReport
            )
         {
            return;
         }

         await _uow.LocationHistory.InsertOneAsync(
            session,
            new(
               location.Id,
               location.Name != request.Name ? request.Name : string.Empty,
               location.MacAddress != request.MacAddress ? request.MacAddress : string.Empty,
               request.IncludeReport,
               request.IsActive,
               createdBy.Id
            ),
            cancellationToken: cancellationToken
         );
      }
   }
}
