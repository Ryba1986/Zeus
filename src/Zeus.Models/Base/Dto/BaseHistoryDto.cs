using System;

namespace Zeus.Models.Base.Dto
{
   public abstract class BaseHistoryDto
   {
      public bool IsActive { get; init; }
      public string CreatedByName { get; init; }
      public DateTime CreateDate { get; init; }

      public BaseHistoryDto()
      {
         CreatedByName = string.Empty;
      }
   }
}
