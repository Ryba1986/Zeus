using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
   internal sealed class CreateLocationHandler : BaseRequestHandler, IRequestHandler<CreateLocationCommand, Result>
   {
      private readonly ZeusSettings _settings;

      public CreateLocationHandler(UnitOfWork uow, ZeusSettings settings) : base(uow)
      {
         _settings = settings;
      }

      public async Task<Result> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
      {
         User? createdBy = await _uow.User
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Id == request.CreatedById &&
               x.IsActive
            , cancellationToken);

         if (createdBy is null)
         {
            return Result.Error($"Cannot find an active user with id {request.CreatedById}.");
         }

         Location? existingLocation = await _uow.Location
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
               x.Name == request.Name || x.MacAddress == request.MacAddress
            , cancellationToken);

         if (existingLocation?.Name == request.Name)
         {
            return Result.Error($"Location with name {request.Name} exist.");
         }
         if (existingLocation?.MacAddress == request.MacAddress)
         {
            return Result.Error($"Location with mac address {request.MacAddress} exist.");
         }
         if (request.IsActive && await LocationHandlerHelper.IsLimitLocations(_uow, _settings.LocationLimit, cancellationToken))
         {
            return Result.Error("The limit of active locations has been exceeded.");
         }

         Location newLocation = new(request.Name, request.MacAddress, request.IncludeReport, request.IsActive);

         return await _uow.ExecuteTransactionAsync(
            async token =>
            {
               _uow.Location.Add(newLocation);
               await _uow.SaveChangesAsync(token);

               _uow.LocationHistory.Add(new(newLocation.Id, newLocation.Name, newLocation.MacAddress, newLocation.IncludeReport, newLocation.IsActive, createdBy.Id));
               await _uow.SaveChangesAsync(token);
            },
            cancellationToken
         );
      }
   }
}
