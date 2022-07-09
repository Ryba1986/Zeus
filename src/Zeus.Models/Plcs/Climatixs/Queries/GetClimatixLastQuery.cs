using MediatR;
using Zeus.Models.Plcs.Climatixs.Dto;

namespace Zeus.Models.Plcs.Climatixs.Queries
{
   public sealed class GetClimatixLastQuery : IRequest<ClimatixDto?>
   {
      public int DeviceId { get; init; }
   }
}
