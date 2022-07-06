using System;
using Zeus.Utilities.Extensions;

namespace Zeus.Domain.Base
{
   public abstract class BaseHistory
   {
      public int Id { get; init; }
      public bool IsActive { get; init; }
      public int CreatedById { get; init; }
      public DateTime CreateDate { get; init; }

      public BaseHistory(bool isActive, int createdById)
      {
         CreateDate = DateTime.Now.RoundToSecond();

         IsActive = isActive;
         CreatedById = createdById;
      }
   }
}
