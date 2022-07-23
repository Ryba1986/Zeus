using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using MediatR;
using RestSharp;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Climatixs.Commands;

namespace Zeus.Client.Handlers.Plcs.Climatixs.Commands
{
   internal sealed class CreateClimatixHandler : BaseRequestStorageHandler, IRequestHandler<CreateClimatixCommand, Result>
   {
      public CreateClimatixHandler(RestClient client, LiteDatabase database) : base(client, database)
      {
      }

      public Task<Result> Handle(CreateClimatixCommand request, CancellationToken cancellationToken)
      {
         return CreatePlcAsync("climatix/createClimatix", request, cancellationToken);
      }
   }
}
