using System;

namespace Zeus.Models.Base.Dto
{
   public abstract class BaseDto
   {
      public int Id { get; init; }
      public bool IsActive { get; init; }
      public byte[] Version { get; init; }

      public BaseDto()
      {
         Version = Array.Empty<byte>();
      }
   }
}
