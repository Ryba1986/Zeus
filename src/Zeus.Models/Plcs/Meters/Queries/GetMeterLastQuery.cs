using MediatR;
using Zeus.Models.Plcs.Meters.Dto;

namespace Zeus.Models.Plcs.Meters.Queries
{
   public sealed class GetMeterLastQuery : IRequest<MeterDto>
   {
      public int DeviceId { get; init; }
   }
}
