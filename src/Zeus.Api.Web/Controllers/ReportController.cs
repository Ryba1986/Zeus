using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeus.Models.Reports.Queries;

namespace Zeus.Api.Web.Controllers
{
   public sealed class ReportController : BaseController
   {
      public ReportController(IMediator mediator) : base(mediator)
      {
      }

      [HttpGet]
      public async Task<IActionResult> GetReportTypeDictionary()
      {
         return await SendAsync(new GetReportTypeDictionaryQuery());
      }

      [HttpGet]
      public async Task<IActionResult> GetReport([FromQuery] GetReportQuery request)
      {
         return await SendAsync(request);
      }
   }
}
