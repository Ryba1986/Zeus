using MediatR;
using Zeus.Models.Plcs.Rvds.Dto;

namespace Zeus.Models.Plcs.Rvds.Queries
{
   public sealed class GetRvd145LastQuery : IRequest<Rvd145Dto?>
   {
      public int DeviceId { get; init; }
   }
}
