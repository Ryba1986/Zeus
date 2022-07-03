using System.Collections.Generic;
using MediatR;
using Zeus.Models.Users.Dto;

namespace Zeus.Models.Users.Queries
{
   public sealed class GetUsersQuery : IRequest<IEnumerable<UserDto>>
   {
   }
}
