using System.Collections.Generic;
using MediatR;
using Zeus.Models.Users.Dto;

namespace Zeus.Models.Users.Queries
{
   public sealed class GetUserHistoryQuery : IRequest<IReadOnlyCollection<UserHistoryDto>>
   {
      public int UserId { get; init; }
   }
}
