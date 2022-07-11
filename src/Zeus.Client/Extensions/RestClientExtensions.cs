using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using Zeus.Models.Base;

namespace Zeus.Client.Extensions
{
   internal static class RestClientExtensions
   {
      public static async Task<Result> PostAsync(this RestClient client, string url, IRequest<Result>? command, CancellationToken cancellationToken)
      {
         RestRequest request = new(url);
         if (command is not null)
         {
            request.AddJsonBody(command);
         }

         RestResponse<Result> response = await client.ExecutePostAsync<Result>(request, cancellationToken);

         return response.IsSuccessful
            ? response.Data
            : Result.Error(response.ErrorMessage ?? string.Empty);
      }
   }
}
