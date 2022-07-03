using MediatR;

namespace Zeus.Models.Base.Commands
{
   public abstract class BaseUpdateCommand : IRequest<Result>
   {
      public int Id { get; init; }
      public bool IsActive { get; init; }
      public int ModifiedById { get; private set; }
      public short Version { get; init; }

      public void Update(int modifiedById)
      {
         ModifiedById = modifiedById;
      }
   }
}
