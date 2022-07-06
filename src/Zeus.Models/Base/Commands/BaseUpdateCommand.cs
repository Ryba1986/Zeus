using System;
using MediatR;

namespace Zeus.Models.Base.Commands
{
   public abstract class BaseUpdateCommand : IRequest<Result>
   {
      public int Id { get; init; }
      public bool IsActive { get; init; }
      public int ModifiedById { get; private set; }
      public byte[] Version { get; init; }

      public BaseUpdateCommand()
      {
         Version = Array.Empty<byte>();
      }

      public void Update(int modifiedById)
      {
         ModifiedById = modifiedById;
      }
   }
}
