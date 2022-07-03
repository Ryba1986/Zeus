using Zeus.Utilities.Helpers;

namespace Zeus.Domain.Base
{
   public abstract class BaseDomain
   {
      public int Id { get; init; }
      public bool IsActive { get; protected set; }
      public short Version { get; protected set; }

      public BaseDomain(bool isActive)
      {
         Id = RandomHelper.CreateInt();
         Version = RandomHelper.CreateShort();

         IsActive = isActive;
      }
   }
}
