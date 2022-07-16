using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using Zeus.Client.Extensions;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Climatixs.Commands;

namespace Zeus.Client.Handlers.Plcs.Climatixs.Commands
{
   internal sealed class CreateClimatixHandler : BaseRequestHandler, IRequestHandler<CreateClimatixCommand, Result>
   {
      public CreateClimatixHandler(RestClient client) : base(client)
      {
      }

      public async Task<Result> Handle(CreateClimatixCommand request, CancellationToken cancellationToken)
      {
         Result result = await _client.PostAsync("climatix/createClimatix", request, cancellationToken);
         if (!result.IsSuccess)
         {
            // TODO: add local storage logic
         }

         return result;
      }
   }
}
