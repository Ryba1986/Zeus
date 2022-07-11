using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Zeus.Models.Base;

namespace Zeus.Client.Extensions
{
   internal static class RestClientExtensions
   {
      public static async Task<Result> PostAsync<T>(this RestClient client, string url, T? data, CancellationToken cancellationToken) where T : class
      {
         RestRequest request = new(url);
         if (data is not null)
         {
            request.AddJsonBody(data);
         }

         RestResponse<Result> response = await client.ExecutePostAsync<Result>(request, cancellationToken);

         return response.IsSuccessful
            ? response.Data
            : Result.Error(response.ErrorMessage ?? string.Empty);
      }
   }
}
