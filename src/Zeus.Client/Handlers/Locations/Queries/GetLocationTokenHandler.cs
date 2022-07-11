using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using Zeus.Client.Extensions;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Base;
using Zeus.Models.Locations.Queries;

namespace Zeus.Client.Handlers.Locations.Queries
{
   internal sealed class GetLocationTokenHandler : BaseRequestHandler, IRequestHandler<GetLocationTokenQuery, Result>
   {
      public GetLocationTokenHandler(RestClient client) : base(client)
      {
      }

      public async Task<Result> Handle(GetLocationTokenQuery request, CancellationToken cancellationToken)
      {
         Result result = await _client.PostAsync("location/getLocationToken", request, cancellationToken);
         if (!result.IsSuccess)
         {
            _client.Authenticator = null;
            return result;
         }

         _client.Authenticator = new JwtAuthenticator(result.Value);
         return Result.Success();
      }
   }
}
