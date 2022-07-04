using System;

namespace Zeus.Models.Reports.Dto
{
   public readonly struct ReportFileDto
   {
      public string Name { get; init; }
      public string Type { get; init; }
      public byte[] Content { get; init; }


      public ReportFileDto()
      {
         Name = string.Empty;
         Type = string.Empty;
         Content = Array.Empty<byte>();
      }
   }
}
