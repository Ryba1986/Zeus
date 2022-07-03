using MediatR;
using Zeus.Models.Base;

namespace Zeus.Models.Locations.Queries
{
   public sealed class GetLocationTokenQuery : IRequest<Result>
   {
      public string MacAddress { get; init; }
      public string Hostname { get; init; }
      public string ClientVersion { get; init; }

      public GetLocationTokenQuery()
      {
         MacAddress = string.Empty;
         Hostname = string.Empty;
         ClientVersion = string.Empty;
      }
   }
}
