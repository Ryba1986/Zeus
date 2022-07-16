using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestSharp;
using Zeus.Models.Base;

namespace Zeus.Client.Extensions
{
   internal static class RestClientExtensions
   {

      public static async Task<T?> GetAsync<T>(this RestClient client, string url, IRequest<T> query, CancellationToken cancellationToken) where T : class
      {
         RestRequest request = new(url);
         request.AddJsonBody(query);

         RestResponse<T> response = await client.ExecuteGetAsync<T>(request, cancellationToken);

         return response.IsSuccessful
            ? response.Data
            : null;
      }

      public static async Task<Result> PostAsync(this RestClient client, string url, IRequest<Result> command, CancellationToken cancellationToken)
      {
         RestRequest request = new(url);
         request.AddJsonBody(command);

         RestResponse<Result> response = await client.ExecutePostAsync<Result>(request, cancellationToken);

         return response.IsSuccessful
            ? response.Data
            : Result.Error(response.ErrorMessage ?? string.Empty);
      }
   }
}
