using MediatR;

namespace Zeus.Models.Base.Commands
{
   public abstract class BaseCreateCommand : IRequest<Result>
   {
      public bool IsActive { get; init; }
      public int CreatedById { get; private set; }

      public void Update(int createdById)
      {
         CreatedById = createdById;
      }
   }
}
