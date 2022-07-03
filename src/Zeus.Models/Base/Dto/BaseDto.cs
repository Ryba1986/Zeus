namespace Zeus.Models.Base.Dto
{
   public abstract class BaseDto
   {
      public int Id { get; init; }
      public bool IsActive { get; init; }
      public short Version { get; init; }
   }
}
