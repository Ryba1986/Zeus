using System;

namespace Zeus.Domain.Base
{
   public abstract class BaseDomain
   {
      public int Id { get; init; }
      public bool IsActive { get; protected set; }
      public byte[] Version { get; init; }

      public BaseDomain(bool isActive)
      {
         Version = Array.Empty<byte>();

         IsActive = isActive;
      }
   }
}
