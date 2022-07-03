using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeus.Api.Web.Attributes;
using Zeus.Enums.Users;
using Zeus.Models.Users.Commands;
using Zeus.Models.Users.Queries;

namespace Zeus.Api.Web.Controllers
{
   public sealed class UserController : BaseController
   {
      public UserController(IMediator mediator) : base(mediator)
      {
      }

      [Authorization(UserRole.Admin)]
      [HttpGet]
      public async Task<IActionResult> GetUsers()
      {
         return await SendAsync(new GetUsersQuery());
      }

      [Authorization(UserRole.Admin)]
      [HttpGet]
      public async Task<IActionResult> GetUserHistory([FromQuery] GetUserHistoryQuery request)
      {
         return await SendAsync(request);
      }

      [HttpGet]
      public async Task<IActionResult> GetUserRoleDictionary()
      {
         return await SendAsync(new GetUserRoleDictionaryQuery());
      }

      [AllowAnonymous]
      [HttpPost]
      public async Task<IActionResult> GetUserToken([FromBody] GetUserTokenQuery request)
      {
         return await SendAsync(request);
      }

      [HttpPost]
      public async Task<IActionResult> GetUserTokenRefresh()
      {
         return await SendAsync(new GetUserTokenRefreshQuery()
         {
            UserId = GetUserId()
         });
      }

      [Authorization(UserRole.Admin)]
      [HttpPost]
      public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request)
      {
         request.Update(GetUserId());
         return await SendAsync(request);
      }

      [Authorization(UserRole.Admin)]
      [HttpPost]
      public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
      {
         request.Update(GetUserId());
         return await SendAsync(request);
      }

      [AllowAnonymous]
      [HttpPost]
      public async Task<IActionResult> ChangePasswordUser([FromBody] ChangePasswordUserCommand request)
      {
         return await SendAsync(request);
      }
   }
}
