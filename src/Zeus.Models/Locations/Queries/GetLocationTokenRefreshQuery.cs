using MediatR;
using Zeus.Models.Base;

namespace Zeus.Models.Locations.Queries
{
   public sealed class GetLocationTokenRefreshQuery : IRequest<Result>
   {
      public int LocationId { get; init; }
   }
}
