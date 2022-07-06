using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zeus.Infrastructure.Repositories;

namespace Zeus.Infrastructure.Helpers
{
   internal static class LocationHandlerHelper
   {
      public static async Task<bool> IsLimitLocations(UnitOfWork uow, int locationLimit, CancellationToken cancellationToken)
      {
         int result = await uow.Location
            .AsNoTracking()
            .CountAsync(x =>
               x.IsActive
            , cancellationToken);

         return result >= locationLimit;
      }
   }
}
