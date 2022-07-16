using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using Zeus.Client.Extensions;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Rvds.Commands;

namespace Zeus.Client.Handlers.Plcs.Rvds.Commands
{
   internal sealed class CreateRvd145Handler : BaseRequestHandler, IRequestHandler<CreateRvd145Command, Result>
   {
      public CreateRvd145Handler(RestClient client) : base(client)
      {
      }

      public async Task<Result> Handle(CreateRvd145Command request, CancellationToken cancellationToken)
      {
         Result result = await _client.PostAsync("rvd145/createRvd145", request, cancellationToken);
         if (!result.IsSuccess)
         {
            // TODO: add local storage logic
         }

         return result;
      }
   }
}
