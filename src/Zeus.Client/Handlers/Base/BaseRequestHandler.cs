using RestSharp;

namespace Zeus.Client.Handlers.Base
{
   internal abstract class BaseRequestHandler
   {
      protected readonly RestClient _client;

      public BaseRequestHandler(RestClient client)
      {
         _client = client;
      }
   }
}
