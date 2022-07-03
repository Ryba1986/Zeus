using MediatR;
using Zeus.Models.Base;

namespace Zeus.Models.Users.Queries
{
   public sealed class GetUserTokenRefreshQuery : IRequest<Result>
   {
      public int UserId { get; init; }
   }
}
