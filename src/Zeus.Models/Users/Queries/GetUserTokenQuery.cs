using MediatR;
using Zeus.Models.Base;

namespace Zeus.Models.Users.Queries
{
   public sealed class GetUserTokenQuery : IRequest<Result>
   {
      public string Email { get; init; }
      public string Password { get; init; }

      public GetUserTokenQuery()
      {
         Email = string.Empty;
         Password = string.Empty;
      }
   }
}
