using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using MediatR;
using RestSharp;
using Zeus.Client.Handlers.Base;
using Zeus.Models.Base;
using Zeus.Models.Plcs.Rvds.Commands;

namespace Zeus.Client.Handlers.Plcs.Rvds.Commands
{
   internal sealed class CreateRvd145Handler : BaseRequestStorageHandler, IRequestHandler<CreateRvd145Command, Result>
   {
      public CreateRvd145Handler(RestClient client, LiteDatabase database) : base(client, database)
      {
      }

      public Task<Result> Handle(CreateRvd145Command request, CancellationToken cancellationToken)
      {
         return CreatePlcAsync("rvd145/createRvd145", request, cancellationToken);
      }
   }
}
