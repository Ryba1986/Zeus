using System;
using Zeus.Utilities.Extensions;
using Zeus.Utilities.Helpers;

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
         Id = RandomHelper.CreateInt();
         CreateDate = DateTime.Now.RoundToSecond();

         IsActive = isActive;
         CreatedById = createdById;
      }
   }
}
